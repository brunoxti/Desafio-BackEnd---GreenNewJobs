using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.RentalsUseCases.GetAllRentals
{
    public class GetAllRentalsUseCase
    {
        private readonly IRentalRepository _rentalRepository;

        public GetAllRentalsUseCase(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<IEnumerable<Rental>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var rentals = await _rentalRepository.GetAllAsync();

            if (rentals == null || !rentals.Any())
            {
                return Result<IEnumerable<Rental>>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Rentals", "No rentals found")
                });
            }

            return Result<IEnumerable<Rental>>.SuccessResponse(rentals);
        }
    }
}
