using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotocycleById
{
    public class GetMotorcycleGreenByIdUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;

        public GetMotorcycleGreenByIdUseCase(IMotorcycleGreenRepository motorcycleGreenRepository)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
        }

        public async Task<Result<GetMotorcycleGreenByIdOutput>> ExecuteAsync(GetMotorcycleGreenByIdInput command, CancellationToken cancellationToken)
        {
            var motorcycleGreen = await _motorcycleGreenRepository.GetByIdAsync(command.Id);

            if (motorcycleGreen == null)
            {
                return Result<GetMotorcycleGreenByIdOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Motorcycle not found")
                });
            }

            var output = GetMotorcycleGreenByIdOutput.FromEntity(motorcycleGreen);
            return Result<GetMotorcycleGreenByIdOutput>.SuccessResponse(output);
        }
    }
}
