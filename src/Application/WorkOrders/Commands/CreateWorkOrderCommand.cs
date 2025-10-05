using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.WorkOrders;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CreateWorkOrderCommand : IRequest<WorkOrder>
{
    public required string WorkOrderNumber { get; init; } 
    public required Guid EquipmentId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required WorkOrderPriority Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}

public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateWorkOrderCommandHandler(IWorkOrderRepository workOrderRepository, IEquipmentRepository equipmentRepository)
    {
        _workOrderRepository = workOrderRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<WorkOrder> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(request.EquipmentId, cancellationToken);
        if (equipment is null)
            throw new KeyNotFoundException($"Equipment with id {request.EquipmentId} not found.");

        var wo = WorkOrder.New(
            Guid.NewGuid(),
            request.WorkOrderNumber,
            request.EquipmentId,
            request.Title,
            request.Description,
            request.Priority,
            request.ScheduledDate);

        var created = await _workOrderRepository.AddAsync(wo, cancellationToken);
        return created;
    }
}