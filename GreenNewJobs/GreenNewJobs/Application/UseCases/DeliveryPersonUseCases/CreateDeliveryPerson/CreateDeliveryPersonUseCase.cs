using FluentValidation;
using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson
{
    public class CreateDeliveryPersonUseCase
    {
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<CreateDeliveryPersonUseCase> _logger;
        private readonly IValidator<CreateDeliveryPersonInput> _validator;

        public CreateDeliveryPersonUseCase(IDeliveryPersonRepository driverRepository, IEventDispatcher eventDispatcher, ILogger<CreateDeliveryPersonUseCase> logger, IValidator<CreateDeliveryPersonInput> validator)
        {
            _driverRepository = driverRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<CreateDeliveryPersonOutput>> ExecuteAsync(CreateDeliveryPersonInput input, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(input, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                return Result<CreateDeliveryPersonOutput>.Failure(validationResult.Errors);
            }

            var driver = DeliveryPerson.Create(input.Name, input.CNPJ, input.BirthDate, input.CNHNumber, input.CNHType, false);
            await _driverRepository.AddAsync(driver);

            var driverCreatedEvent = new DeliveryPersonCreatedEvent(driver.Id, driver.Name, driver.CNPJ, driver.BirthDate, driver.CNHNumber, driver.CNHType, driver.CNHImagePath, driver.CreatedAt);
            await _eventDispatcher.Dispatch(driverCreatedEvent);

            _logger.LogInformation($"DeliveryPerson created successfully: {driver.Id}");

            var output = CreateDeliveryPersonOutput.FromEntity(driver);
            return Result<CreateDeliveryPersonOutput>.SuccessResponse(output);
        }
    }
}
