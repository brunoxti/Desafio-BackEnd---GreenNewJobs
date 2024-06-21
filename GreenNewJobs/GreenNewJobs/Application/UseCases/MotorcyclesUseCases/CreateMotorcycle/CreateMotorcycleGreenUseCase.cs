using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.CreateMotorcycle
{
    public class CreateMotorcycleGreenUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IValidator<CreateMotorcycleGreenInput> _validator;

        public CreateMotorcycleGreenUseCase(IMotorcycleGreenRepository motorcycleGreenRepository, IEventDispatcher eventDispatcher, IValidator<CreateMotorcycleGreenInput> validator)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
            _eventDispatcher = eventDispatcher;
            _validator = validator;
        }

        public async Task<Result<CreateMotorcycleGreenOutput>> ExecuteAsync(CreateMotorcycleGreenInput input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                return Result<CreateMotorcycleGreenOutput>.Failure(validationResult.Errors);
            }

            var motorcycleGreen = MotorcycleGreen.Create(input.Model, input.Year, input.Plate);

            await _motorcycleGreenRepository.AddAsync(motorcycleGreen);

            var motorcycleGreenCreatedEvent = new MotorcycleGreenCreatedEvent(motorcycleGreen.Id, motorcycleGreen.Model, motorcycleGreen.Year, motorcycleGreen.Plate, motorcycleGreen.Available, motorcycleGreen.CreatedAt);
            await _eventDispatcher.Dispatch(motorcycleGreenCreatedEvent);

            var output = CreateMotorcycleGreenOutput.FromEntity(motorcycleGreen);
            return Result<CreateMotorcycleGreenOutput>.SuccessResponse(output);
        }
    }
}
