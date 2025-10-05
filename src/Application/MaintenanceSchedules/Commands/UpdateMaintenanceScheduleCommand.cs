using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record UpdateMaintenanceScheduleCommand : IRequest<MaintenanceSchedule>
{
    public required Guid Id { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required MaintenanceFrequency Frequency { get; init; }
    public required DateTime NextDueDate { get; init; }
}

public class UpdateMaintenanceScheduleCommandHandler : IRequestHandler<UpdateMaintenanceScheduleCommand, MaintenanceSchedule>
{
    private readonly IMaintenanceScheduleRepository _scheduleRepository;

    public UpdateMaintenanceScheduleCommandHandler(IMaintenanceScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<MaintenanceSchedule> Handle(UpdateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var existing = await _scheduleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"Maintenance schedule with id {request.Id} not found.");

        existing.UpdateSchedule(request.TaskName, request.Description, request.Frequency, request.NextDueDate);
        await _scheduleRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}