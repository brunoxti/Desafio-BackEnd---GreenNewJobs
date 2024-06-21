using FluentValidation;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotorcycleByPlate;

namespace GreenNewJobs.Application.Validators
{
    public class GetMotorcycleGreenByPlateInputValidator : AbstractValidator<GetMotorcycleGreenByPlateInput>
    {
        public GetMotorcycleGreenByPlateInputValidator()
        {
            RuleFor(x => x.Plate).NotEmpty().WithMessage("Plate is required.");
        }
    }
}
