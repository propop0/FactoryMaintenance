using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CompleteWorkOrderCommand(Guid Id, string CompletionNotes) : IRequest<Domain.WorkOrders.WorkOrder?>;

public class CompleteWorkOrderCommandHandler : IRequestHandler<CompleteWorkOrderCommand, Domain.WorkOrders.WorkOrder?>
{
    private readonly IWorkOrderRepository _workOrderRepository;

    public CompleteWorkOrderCommandHandler(IWorkOrderRepository workOrderRepository)
    {
        _workOrderRepository = workOrderRepository;
    }

    public async Task<Domain.WorkOrders.WorkOrder?> Handle(CompleteWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"WorkOrder with id {request.Id} not found.");

        existing.Complete(request.CompletionNotes);
        await _workOrderRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}