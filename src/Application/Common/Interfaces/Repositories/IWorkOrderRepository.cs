using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.WorkOrders;

namespace Application.Common.Interfaces.Repositories;

public interface IWorkOrderRepository
{
    Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}