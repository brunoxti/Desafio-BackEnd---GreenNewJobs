using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.UpdateDeliveryPerson
{
    public class UpdateDeliveryPersonUseCase
    {
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly ILogger<UpdateDeliveryPersonUseCase> _logger;

        public UpdateDeliveryPersonUseCase(IDeliveryPersonRepository driverRepository, ILogger<UpdateDeliveryPersonUseCase> logger)
        {
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Result<string>> ExecuteAsync(UpdateDeliveryPersonInput input, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(input.Id);
            if (driver == null)
            {
                _logger.LogWarning($"DeliveryPerson with id {input.Id} not found.");
                return Result<string>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "DeliveryPerson not found")
                });
            }

            driver.Update(input.Name, input.CNPJ, input.BirthDate, input.CNHNumber, input.CNHType, false);

            if (!string.IsNullOrEmpty(input.CNHImagePath))
            {
                driver.UpdateCNHImagePath(input.CNHImagePath);
            }

            await _driverRepository.UpdateAsync(driver);
            _logger.LogInformation($"DeliveryPerson with id {driver.Id} updated successfully.");

            return Result<string>.SuccessResponse("");
        }
    }
}
