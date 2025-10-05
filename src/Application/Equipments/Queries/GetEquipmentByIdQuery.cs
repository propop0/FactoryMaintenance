using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.Equipments;
using MediatR;

namespace Application.Equipments.Queries;

public record GetEquipmentByIdQuery(Guid Id) : IRequest<Equipment?>;

public class GetEquipmentByIdQueryHandler : IRequestHandler<GetEquipmentByIdQuery, Equipment?>
{
    private readonly IEquipmentQueries _equipmentQueries;

    public GetEquipmentByIdQueryHandler(IEquipmentQueries equipmentQueries)
    {
        _equipmentQueries = equipmentQueries;
    }

    public async Task<Equipment?> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _equipmentQueries.GetByIdAsync(request.Id, cancellationToken);
    }
}