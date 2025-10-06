using System;
using System.Threading;
using FluentValidation;
using Application.Common.Interfaces.Repositories;

namespace Application.Equipments.Commands
{
    public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
    {
        public CreateEquipmentCommandValidator(IEquipmentRepository equipmentRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required")
                .MaximumLength(50);

            RuleFor(x => x.SerialNumber)
                .NotEmpty().WithMessage("SerialNumber is required")
                .MaximumLength(50)
                .MustAsync(async (serial, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(serial)) return false;
                    return !await equipmentRepository.ExistsBySerialNumberAsync(serial, ct);
                })
                .WithMessage("SerialNumber must be unique");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(200);

            RuleFor(x => x.InstallationDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Installation date cannot be in the future");
        }
    }
}