using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record DeactivateMaintenanceScheduleCommand(Guid Id) : IRequest<Unit>;

public class DeactivateMaintenanceScheduleCommandHandler : IRequestHandler<DeactivateMaintenanceScheduleCommand, Unit>
{
    private readonly IMaintenanceScheduleRepository _scheduleRepository;

    public DeactivateMaintenanceScheduleCommandHandler(IMaintenanceScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Unit> Handle(DeactivateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            throw new KeyNotFoundException($"Maintenance schedule with id {request.Id} not found.");
        }

        schedule.Deactivate();
        await _scheduleRepository.UpdateAsync(schedule, cancellationToken);

        return Unit.Value;
    }
}