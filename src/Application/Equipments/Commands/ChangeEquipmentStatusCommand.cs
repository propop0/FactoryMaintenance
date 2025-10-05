using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Equipments;
using MediatR;

namespace Application.Equipments.Commands;

public record ChangeEquipmentStatusCommand : IRequest<Equipment>
{
    public required Guid Id { get; init; }
    public required EquipmentStatus NewStatus { get; init; }
}

public class ChangeEquipmentStatusCommandHandler : IRequestHandler<ChangeEquipmentStatusCommand, Equipment>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public ChangeEquipmentStatusCommandHandler(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<Equipment> Handle(ChangeEquipmentStatusCommand request, CancellationToken cancellationToken)
    {
        var existing = await _equipmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"Equipment with id {request.Id} not found.");

        existing.ChangeStatus(request.NewStatus);
        await _equipmentRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}