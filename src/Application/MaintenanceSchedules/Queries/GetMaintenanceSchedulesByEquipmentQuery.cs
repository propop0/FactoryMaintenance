using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.MaintenanceSchedules.Queries;

public record GetMaintenanceSchedulesByEquipmentQuery(Guid EquipmentId) : IRequest<IReadOnlyList<MaintenanceSchedule>>;

public class GetMaintenanceSchedulesByEquipmentQueryHandler : IRequestHandler<GetMaintenanceSchedulesByEquipmentQuery, IReadOnlyList<MaintenanceSchedule>>
{
    private readonly IMaintenanceScheduleQueries _queries;

    public GetMaintenanceSchedulesByEquipmentQueryHandler(IMaintenanceScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> Handle(GetMaintenanceSchedulesByEquipmentQuery request, CancellationToken cancellationToken)
    {
        return await _queries.GetByEquipmentIdAsync(request.EquipmentId, cancellationToken);
    }
}