using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Queries;

public record GetMaintenanceScheduleByIdQuery(Guid Id) : IRequest<MaintenanceSchedule?>;

public class GetMaintenanceScheduleByIdQueryHandler : IRequestHandler<GetMaintenanceScheduleByIdQuery, MaintenanceSchedule?>
{
    private readonly IMaintenanceScheduleQueries _queries;

    public GetMaintenanceScheduleByIdQueryHandler(IMaintenanceScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<MaintenanceSchedule?> Handle(GetMaintenanceScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _queries.GetByIdAsync(request.Id, cancellationToken);
    }
}