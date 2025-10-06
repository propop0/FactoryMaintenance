using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Equipments.Commands;

public record DeleteEquipmentCommand(Guid Id) : IRequest<Unit>;

public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, Unit>
{
    private readonly IEquipmentRepository _repo;

    public DeleteEquipmentCommandHandler(IEquipmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
            throw new KeyNotFoundException($"Equipment with id {request.Id} not found.");

        await _repo.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}