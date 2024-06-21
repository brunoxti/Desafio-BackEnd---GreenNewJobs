using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRental
{
    public class UpdateRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;

        public UpdateRentalUseCase(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<string>> ExecuteAsync(UpdateRentalInput command, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(command.Id);
            if (rental == null)
            {
                return Result<string>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Rental not found")
                });
            }

            rental.Update(command.EndDate, command.ExpectedEndDate, command.Cost);
            await _rentalRepository.UpdateAsync(rental);

            return Result<string>.SuccessResponse("Rental updated successfully");
        }
    }
}
