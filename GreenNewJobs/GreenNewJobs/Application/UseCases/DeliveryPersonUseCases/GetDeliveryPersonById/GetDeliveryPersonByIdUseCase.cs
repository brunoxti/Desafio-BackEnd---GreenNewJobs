using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Domain.Entities;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetDeliveryPersonById
{
    public class GetDeliveryPersonByIdUseCase
    {
        private readonly IDeliveryPersonRepository _driverRepository;

        public GetDeliveryPersonByIdUseCase(IDeliveryPersonRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Result<DeliveryPerson>> ExecuteAsync(GetDeliveryPersonByIdInput command, CancellationToken cancellationToken)
        {
            var deliveryPerson = await _driverRepository.GetByIdAsync(command.Id);

            if (deliveryPerson == null)
            {
                return Result<DeliveryPerson>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "DeliveryPerson not found")
                });
            }

            return Result<DeliveryPerson>.SuccessResponse(deliveryPerson);
        }
    }
}
