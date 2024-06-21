using FluentValidation;
using GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental;

namespace GreenNewJobs.Application.Validators
{
    public class CreateRentalInputValidator : AbstractValidator<CreateRentalInput>
    {
        public CreateRentalInputValidator()
        {
            RuleFor(x => x.DeliveryPersonId).NotEmpty().WithMessage("DeliveryPersonId is required.");
            RuleFor(x => x.MotorcycleGreenId).NotEmpty().WithMessage("MotorcycleGreenId is required.");
            RuleFor(x => x.PlanId).NotEmpty().WithMessage("PlanId is required.");
        }
    }
}
