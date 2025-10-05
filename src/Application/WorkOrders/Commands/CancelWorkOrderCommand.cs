using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CancelWorkOrderCommand(Guid Id) : IRequest<Domain.WorkOrders.WorkOrder?>;

public class CancelWorkOrderCommandHandler : IRequestHandler<CancelWorkOrderCommand, Domain.WorkOrders.WorkOrder?>
{
    private readonly IWorkOrderRepository _workOrderRepository;

    public CancelWorkOrderCommandHandler(IWorkOrderRepository workOrderRepository)
    {
        _workOrderRepository = workOrderRepository;
    }

    public async Task<Domain.WorkOrders.WorkOrder?> Handle(CancelWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"WorkOrder with id {request.Id} not found.");

        existing.Cancel();
        await _workOrderRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}