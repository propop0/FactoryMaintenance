using System;
using System.Threading;
using FluentValidation;
using Application.Common.Interfaces.Repositories;

namespace Application.MaintenanceSchedules.Validators;

public class CreateMaintenanceScheduleCommandValidator : AbstractValidator<Application.MaintenanceSchedules.Commands.CreateMaintenanceScheduleCommand>
{
    public CreateMaintenanceScheduleCommandValidator(IEquipmentRepository equipmentRepository)
    {
        RuleFor(x => x.EquipmentId)
            .NotEmpty().WithMessage("EquipmentId is required")
            .MustAsync(async (id, ct) =>
            {
                var eq = await equipmentRepository.GetByIdAsync(id, ct);
                return eq != null;
            }).WithMessage("Equipment with provided id does not exist");

        RuleFor(x => x.TaskName)
            .NotEmpty().WithMessage("TaskName is required")
            .MaximumLength(100).WithMessage("TaskName cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.NextDueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("NextDueDate must be in the future");

        RuleFor(x => x.Frequency)
            .IsInEnum().WithMessage("Frequency is required");
    }
}