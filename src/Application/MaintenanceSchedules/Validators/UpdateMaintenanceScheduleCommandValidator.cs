using System;
using FluentValidation;

namespace Application.MaintenanceSchedules.Commands;

public class UpdateMaintenanceScheduleCommandValidator : AbstractValidator<UpdateMaintenanceScheduleCommand>
{
    public UpdateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

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
