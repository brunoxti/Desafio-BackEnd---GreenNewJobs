using FluentValidation.Results;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetAllDeliveryPersons
{
    public class GetAllDeliveryPersonsUseCase
    {
        private readonly IDeliveryPersonRepository _driverRepository;

        public GetAllDeliveryPersonsUseCase(IDeliveryPersonRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Result<IEnumerable<GetAllDeliveryPersonsOutput>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var deliveryPersons = await _driverRepository.GetAllAsync();

            if (deliveryPersons == null || !deliveryPersons.Any())
            {
                return Result<IEnumerable<GetAllDeliveryPersonsOutput>>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersons", "No delivery persons found")
                });
            }

            var output = deliveryPersons.Select(d => GetAllDeliveryPersonsOutput.FromEntity(d)).ToList();
            return Result<IEnumerable<GetAllDeliveryPersonsOutput>>.SuccessResponse(output);
        }
    }
}
