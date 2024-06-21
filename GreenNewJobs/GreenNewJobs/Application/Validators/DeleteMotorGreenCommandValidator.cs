using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.DeleteMotocycle;

namespace GreenNewJobs.Application.Validators
{
    public class DeleteMotorcycleGreenInputValidator : AbstractValidator<DeleteMotorcycleGreenInput>
    {
        public DeleteMotorcycleGreenInputValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
