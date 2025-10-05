using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Equipments;
using MediatR;

namespace Application.Equipments.Commands
{
    public record CreateEquipmentCommand : IRequest<Equipment>
    {
        public required string Name { get; init; }
        public required string Model { get; init; }
        public required string SerialNumber { get; init; }
        public required string Location { get; init; }
        public required DateTime InstallationDate { get; init; }
    }

    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, Equipment>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateEquipmentCommandHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Equipment> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equipment = Equipment.New(
                Guid.NewGuid(),
                request.Name,
                request.Model,
                request.SerialNumber,
                request.Location,
                request.InstallationDate);

            var created = await _equipmentRepository.AddAsync(equipment, cancellationToken);
            return created;
        }
    }
}