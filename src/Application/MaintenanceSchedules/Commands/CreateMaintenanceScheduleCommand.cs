using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record CreateMaintenanceScheduleCommand : IRequest<MaintenanceSchedule>
{
    public required Guid EquipmentId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required MaintenanceFrequency Frequency { get; init; }
    public required DateTime NextDueDate { get; init; }
}

public class CreateMaintenanceScheduleCommandHandler : IRequestHandler<CreateMaintenanceScheduleCommand, MaintenanceSchedule>
{
    private readonly IEquipmentRepository _equipmentRepository;
    // IMaintenanceScheduleRepository may be implemented later in Infrastructure
    private readonly IMaintenanceScheduleRepository? _scheduleRepository;

    public CreateMaintenanceScheduleCommandHandler(
        IEquipmentRepository equipmentRepository,
        IMaintenanceScheduleRepository? scheduleRepository = null)
    {
        _equipmentRepository = equipmentRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<MaintenanceSchedule> Handle(CreateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        // Optional check: ensure equipment exists
        var equipment = await _equipmentRepository.GetByIdAsync(request.EquipmentId, cancellationToken);
        if (equipment is null)
        {
            throw new KeyNotFoundException($"Equipment with id {request.EquipmentId} not found.");
        }

        var schedule = MaintenanceSchedule.New(
            Guid.NewGuid(),
            request.EquipmentId,
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate);

        if (_scheduleRepository != null)
        {
            var created = await _scheduleRepository.AddAsync(schedule, cancellationToken);
            return created;
        }

        return schedule;
    }
}
