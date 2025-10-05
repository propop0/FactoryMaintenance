using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.WorkOrders;
using MediatR;

namespace Application.WorkOrders.Queries;

public record GetAllWorkOrdersQuery : IRequest<IReadOnlyList<WorkOrder>>;

public class GetAllWorkOrdersQueryHandler : IRequestHandler<GetAllWorkOrdersQuery, IReadOnlyList<WorkOrder>>
{
    private readonly IWorkOrderQueries _workOrderQueries;

    public GetAllWorkOrdersQueryHandler(IWorkOrderQueries workOrderQueries)
    {
        _workOrderQueries = workOrderQueries;
    }

    public async Task<IReadOnlyList<WorkOrder>> Handle(GetAllWorkOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _workOrderQueries.GetAllAsync(cancellationToken);
    }
}