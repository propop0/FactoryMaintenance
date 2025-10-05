using System;
using FluentValidation;

namespace Application.MaintenanceSchedules.Validators;

public class CreateMaintenanceScheduleCommandValidator : AbstractValidator<Application.MaintenanceSchedules.Commands.CreateMaintenanceScheduleCommand>
{
    public CreateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.EquipmentId)
            .NotEmpty().WithMessage("EquipmentId is required");

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