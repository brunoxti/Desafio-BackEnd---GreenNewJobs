using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental
{
    public class CreateRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<CreateRentalUseCase> _logger;

        public CreateRentalUseCase(
            IRentalRepository rentalRepository,
            IMotorcycleGreenRepository motorcycleGreenRepository,
            IDeliveryPersonRepository driverRepository,
            IRentalPlanRepository rentalPlanRepository,
            IEventDispatcher eventDispatcher,
            ILogger<CreateRentalUseCase> logger)
        {
            _rentalRepository = rentalRepository;
            _motorcycleGreenRepository = motorcycleGreenRepository;
            _driverRepository = driverRepository;
            _rentalPlanRepository = rentalPlanRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task<Result<CreateRentalOutput>> ExecuteAsync(CreateRentalInput input, CancellationToken cancellationToken)
        {
            var motorcycleGreen = await _motorcycleGreenRepository.GetByIdAsync(input.MotorcycleGreenId);
            if (motorcycleGreen == null || !motorcycleGreen.Available)
            {
                return Result<CreateRentalOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("MotorcycleGreenId", "Motorbike not available for rental.")
                });
            }

            var driver = await _driverRepository.GetByIdAsync(input.DeliveryPersonId);
            if (driver == null || !driver.CNHType.Contains("A"))
            {
                return Result<CreateRentalOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersonId", "DeliveryPerson not licensed for category A.")
                });
            }

            var rentalPlan = await _rentalPlanRepository.GetByIdAsync(input.PlanId);
            if (rentalPlan == null)
            {
                return Result<CreateRentalOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("PlanId", "Invalid rental plan.")
                });
            }

            var startDate = DateTime.Now.Date.AddDays(1);
            var endDate = startDate.AddDays(rentalPlan.DurationDays - 1);
            var expectedEndDate = endDate;
            var cost = rentalPlan.CostPerDay * rentalPlan.DurationDays;

            var rental = Rental.Create(input.DeliveryPersonId, input.MotorcycleGreenId, input.PlanId, startDate, endDate, expectedEndDate, cost);

            motorcycleGreen.SetAvailability(false);
            await _motorcycleGreenRepository.UpdateAsync(motorcycleGreen);

            driver.StartRental();
            await _driverRepository.UpdateAsync(driver);

            await _rentalRepository.AddAsync(rental);

            var rentalCreatedEvent = new RentalCreatedEvent(rental.Id, rental.DeliveryPersonId, rental.MotorcycleGreenId, rental.StartDate, rental.EndDate, rental.ExpectedEndDate, rental.Cost, rental.CreatedAt);
            await _eventDispatcher.Dispatch(rentalCreatedEvent);

            var output = CreateRentalOutput.FromEntity(rental);
            return Result<CreateRentalOutput>.SuccessResponse(output);
        }
    }
}
