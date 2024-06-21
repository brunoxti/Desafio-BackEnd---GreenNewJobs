using FluentValidation;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.Validators
{
    public class CreateDeliveryPersonInputValidator : AbstractValidator<CreateDeliveryPersonInput>
    {
        private readonly IDeliveryPersonRepository _driverRepository;

        public CreateDeliveryPersonInputValidator(IDeliveryPersonRepository driverRepository)
        {
            _driverRepository = driverRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("CNPJ is required.")
                .MustAsync(BeUniqueCNPJ).WithMessage("CNPJ already exists.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("BirthDate is required.");

            RuleFor(x => x.CNHNumber)
                .NotEmpty().WithMessage("CNHNumber is required.")
                .MustAsync(BeUniqueCNHNumber).WithMessage("CNH number already exists.");

            RuleFor(x => x.CNHType)
                .NotEmpty().WithMessage("CNHType is required.")
                .Must(BeAValidCNHType).WithMessage("Invalid CNH type. Valid types are A, B, or A+B.");
        }

        private async Task<bool> BeUniqueCNPJ(string cnpj, CancellationToken cancellationToken)
        {
            return !await _driverRepository.ExistsByCNPJAsync(cnpj);
        }

        private async Task<bool> BeUniqueCNHNumber(string cnhNumber, CancellationToken cancellationToken)
        {
            return !await _driverRepository.ExistsByCNHAsync(cnhNumber);
        }

        private bool BeAValidCNHType(string cnhType)
        {
            var validCnhTypes = new HashSet<string> { "A", "B", "A+B" };
            return validCnhTypes.Contains(cnhType);
        }
    }
}
