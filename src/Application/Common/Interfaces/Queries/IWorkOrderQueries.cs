using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.WorkOrders;

namespace Application.Common.Interfaces.Queries;

public interface IWorkOrderQueries
{
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}