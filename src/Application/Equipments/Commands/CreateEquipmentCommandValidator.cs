using FluentValidation;

namespace Application.Equipments.Commands;

public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
{
    public CreateEquipmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MaximumLength(50);

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("SerialNumber is required")
            .MaximumLength(50);

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required")
            .MaximumLength(200);

        RuleFor(x => x.InstallationDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Installation date cannot be in the future");
    }
}