using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Queries;

public record GetAllMaintenanceSchedulesQuery : IRequest<IReadOnlyList<MaintenanceSchedule>>;

public class GetAllMaintenanceSchedulesQueryHandler : IRequestHandler<GetAllMaintenanceSchedulesQuery, IReadOnlyList<MaintenanceSchedule>>
{
    private readonly IMaintenanceScheduleQueries _queries;

    public GetAllMaintenanceSchedulesQueryHandler(IMaintenanceScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> Handle(GetAllMaintenanceSchedulesQuery request, CancellationToken cancellationToken)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}