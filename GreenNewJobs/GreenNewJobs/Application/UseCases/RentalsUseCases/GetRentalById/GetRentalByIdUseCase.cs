using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.RentalsUseCases.GetRentalById
{
    public class GetRentalByIdUseCase
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalByIdUseCase(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<Rental>> ExecuteAsync(GetRentalByIdInput command, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(command.Id);

            if (rental == null)
            {
                return Result<Rental>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Rental not found")
                });
            }

            return Result<Rental>.SuccessResponse(rental);
        }
    }
}
