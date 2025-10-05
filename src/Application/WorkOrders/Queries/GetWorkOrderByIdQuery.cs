using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.WorkOrders;
using MediatR;

namespace Application.WorkOrders.Queries;

public record GetWorkOrderByIdQuery(Guid Id) : IRequest<WorkOrder?>;

public class GetWorkOrderByIdQueryHandler : IRequestHandler<GetWorkOrderByIdQuery, WorkOrder?>
{
    private readonly IWorkOrderQueries _workOrderQueries;

    public GetWorkOrderByIdQueryHandler(IWorkOrderQueries workOrderQueries)
    {
        _workOrderQueries = workOrderQueries;
    }

    public async Task<WorkOrder?> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _workOrderQueries.GetByIdAsync(request.Id, cancellationToken);
    }
}