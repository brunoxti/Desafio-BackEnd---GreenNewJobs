using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.DeleteMotocycle
{
    public class DeleteMotorcycleGreenUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;
        private readonly IRentalRepository _rentalRepository;

        public DeleteMotorcycleGreenUseCase(IMotorcycleGreenRepository motorcycleGreenRepository, IRentalRepository rentalRepository)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<DeleteMotorcycleGreenOutput>> ExecuteAsync(DeleteMotorcycleGreenInput input, CancellationToken cancellationToken)
        {
            var motorcycleGreen = await _motorcycleGreenRepository.GetByIdAsync(input.Id);
            if (motorcycleGreen == null)
            {
                return Result<DeleteMotorcycleGreenOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Motorcycle not found.")
                });
            }

            // Verifica se há registros de locações associados à moto
            var rentals = await _rentalRepository.GetRentalsByMotorcycleGreenIdAsync(input.Id);
            if (rentals.Any())
            {
                return Result<DeleteMotorcycleGreenOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Cannot delete motorcycle with associated rentals.")
                });
            }

            await _motorcycleGreenRepository.DeleteAsync(motorcycleGreen.Id);

            var output = DeleteMotorcycleGreenOutput.FromEntity(motorcycleGreen);
            return Result<DeleteMotorcycleGreenOutput>.SuccessResponse(output);
        }
    }
}
