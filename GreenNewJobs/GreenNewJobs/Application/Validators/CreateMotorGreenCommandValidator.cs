using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.CreateMotorcycle;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.Validators
{
    public class CreateMotorcycleGreenInputValidator : AbstractValidator<CreateMotorcycleGreenInput>
    {
        public CreateMotorcycleGreenInputValidator(IMotorcycleGreenRepository motorcycleGreenRepository)
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(100).WithMessage("Model can't be longer than 100 characters.");

            RuleFor(x => x.Year)
                .GreaterThan(0).WithMessage("Year must be greater than 0.");

            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Plate is required.")
                .MaximumLength(10).WithMessage("Plate can't be longer than 10 characters.")
                .MustAsync(async (plate, cancellation) => !(await motorcycleGreenRepository.PlateExistsAsync(plate)))
                .WithMessage("A motorcycle with this plate already exists.");
        }
    }
}
