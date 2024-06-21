using FluentValidation;
using GreenNewJobs.Application.UseCases.OrdersUseCases.CreateOrder;

namespace GreenNewJobs.Application.Validators
{
    public class CreateOrderInputValidator : AbstractValidator<CreateOrderInput>
    {
        public CreateOrderInputValidator()
        {
            RuleFor(x => x.CreationDate).NotEmpty().WithMessage("CreationDate is required.");
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Value must be greater than 0.");
        }
    }
}
