using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.UpdateMotorcycle;

namespace GreenNewJobs.Application.Validators
{
    public class UpdateMotorcycleGreenInputValidator : AbstractValidator<UpdateMotorcycleGreenInput>
    {
        public UpdateMotorcycleGreenInputValidator()
        {
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required.");
            RuleFor(x => x.Year).GreaterThan(0).WithMessage("Year must be greater than 0.");
            RuleFor(x => x.Plate).NotEmpty().WithMessage("Plate is required.");
        }
    }
}
