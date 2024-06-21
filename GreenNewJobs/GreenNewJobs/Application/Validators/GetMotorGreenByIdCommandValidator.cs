using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotocycleById;

namespace GreenNewJobs.Application.Validators
{
    public class GetMotorcycleGreenByIdInputValidator : AbstractValidator<GetMotorcycleGreenByIdInput>
    {
        public GetMotorcycleGreenByIdInputValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
