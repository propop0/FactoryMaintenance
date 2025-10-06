using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.WorkOrders;
using MediatR;

namespace Application.WorkOrders.Commands;

public record UpdateWorkOrderCommand : IRequest<WorkOrder>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required WorkOrderPriority Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}

public class UpdateWorkOrderCommandHandler : IRequestHandler<UpdateWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _workOrderRepository;

    public UpdateWorkOrderCommandHandler(IWorkOrderRepository workOrderRepository)
    {
        _workOrderRepository = workOrderRepository;
    }

    public async Task<WorkOrder> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"WorkOrder with id {request.Id} not found.");

        existing.UpdateDetails(request.Title, request.Description, request.Priority, request.ScheduledDate);
        await _workOrderRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}