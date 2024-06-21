using FluentValidation;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.UpdateDeliveryPerson;

namespace GreenNewJobs.Application.Validators
{
    public class UpdateDeliveryPersonInputValidator : AbstractValidator<UpdateDeliveryPersonInput>
    {
        public UpdateDeliveryPersonInputValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.CNPJ).NotEmpty().WithMessage("CNPJ is required.");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required.");
            RuleFor(x => x.CNHNumber).NotEmpty().WithMessage("CNHNumber is required.");
            RuleFor(x => x.CNHType).NotEmpty().WithMessage("CNHType is required.");
            RuleFor(x => x.CNHImagePath).NotEmpty().WithMessage("CNHImage is required.");
        }
    }
}
