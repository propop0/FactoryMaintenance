using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.MaintenanceSchedules;

namespace Application.Common.Interfaces.Queries;

public interface IMaintenanceScheduleQueries
{
    Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceSchedule>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<MaintenanceSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}