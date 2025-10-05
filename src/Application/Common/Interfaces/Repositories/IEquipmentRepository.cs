using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Equipments;

namespace Application.Common.Interfaces.Repositories;

public interface IEquipmentRepository
{
    Task<Equipment> AddAsync(Equipment entity, CancellationToken cancellationToken);
    Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken);
    Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Equipment entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken);
}