using GreenNewJobs.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.UpdateMotorcycle
{
    public class UpdateMotorcycleGreenUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;
        private readonly IValidator<UpdateMotorcycleGreenInput> _validator;

        public UpdateMotorcycleGreenUseCase(IMotorcycleGreenRepository motorcycleGreenRepository, IValidator<UpdateMotorcycleGreenInput> validator)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
            _validator = validator;
        }

        public async Task<Result<UpdateMotorcycleGreenOutput>> ExecuteAsync(Guid id, UpdateMotorcycleGreenInput command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result<UpdateMotorcycleGreenOutput>.Failure(validationResult.Errors);
            }

            var motorcycleGreen = await _motorcycleGreenRepository.GetByIdAsync(id);
            if (motorcycleGreen == null)
            {
                return Result<UpdateMotorcycleGreenOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Motorcycle not found.")
                });
            }

            motorcycleGreen.Update(command.Model, command.Year, command.Plate);

            await _motorcycleGreenRepository.UpdateAsync(motorcycleGreen);

            var output = UpdateMotorcycleGreenOutput.FromEntity(motorcycleGreen);
            return Result<UpdateMotorcycleGreenOutput>.SuccessResponse(output);
        }
    }
}
