using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Queries;
using Domain.Equipments;
using MediatR;

namespace Application.Equipments.Queries;

public record GetAllEquipmentQuery : IRequest<IReadOnlyList<Equipment>>;

public class GetAllEquipmentQueryHandler : IRequestHandler<GetAllEquipmentQuery, IReadOnlyList<Equipment>>
{
    private readonly IEquipmentQueries _equipmentQueries;

    public GetAllEquipmentQueryHandler(IEquipmentQueries equipmentQueries)
    {
        _equipmentQueries = equipmentQueries;
    }

    public async Task<IReadOnlyList<Equipment>> Handle(GetAllEquipmentQuery request, CancellationToken cancellationToken)
    {
        return await _equipmentQueries.GetAllAsync(cancellationToken);
    }
}