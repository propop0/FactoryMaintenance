using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Equipments;


namespace Application.Common.Interfaces.Queries
{
    public interface IEquipmentQueries
    {
        Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken);
        Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}