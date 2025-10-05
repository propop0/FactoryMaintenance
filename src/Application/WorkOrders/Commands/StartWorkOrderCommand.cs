using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.WorkOrders.Commands;

public record StartWorkOrderCommand(Guid Id) : IRequest<Domain.WorkOrders.WorkOrder?>;

public class StartWorkOrderCommandHandler : IRequestHandler<StartWorkOrderCommand, Domain.WorkOrders.WorkOrder?>
{
    private readonly IWorkOrderRepository _workOrderRepository;

    public StartWorkOrderCommandHandler(IWorkOrderRepository workOrderRepository)
    {
        _workOrderRepository = workOrderRepository;
    }

    public async Task<Domain.WorkOrders.WorkOrder?> Handle(StartWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"WorkOrder with id {request.Id} not found.");

        existing.StartWork();
        await _workOrderRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}