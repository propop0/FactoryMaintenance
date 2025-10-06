using System;
using System.Threading;
using FluentValidation;
using Application.Common.Interfaces.Repositories;
using Application.WorkOrders.Commands;

namespace Application.WorkOrders.Validators;

public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderCommandValidator(IEquipmentRepository equipmentRepository)
    {
        RuleFor(x => x.EquipmentId)
            .NotEmpty().WithMessage("EquipmentId is required")
            .MustAsync(async (id, ct) =>
            {
                var equipment = await equipmentRepository.GetByIdAsync(id, ct);
                return equipment != null;
            }).WithMessage("Equipment with provided id does not exist");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.ScheduledDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("ScheduledDate must be in the future");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority is required");
    }
}