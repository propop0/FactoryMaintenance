using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Equipments;
using MediatR;

namespace Application.Equipments.Commands;

public record UpdateEquipmentCommand : IRequest<Equipment>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string Location { get; init; }
}

public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, Equipment>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public UpdateEquipmentCommandHandler(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<Equipment> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _equipmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"Equipment with id {request.Id} not found.");

        existing.UpdateDetails(request.Name, request.Model, request.Location);
        await _equipmentRepository.UpdateAsync(existing, cancellationToken);
        return existing;
    }
}