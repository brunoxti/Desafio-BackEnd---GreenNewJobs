using GreenNewJobs.Application.UseCases.RentalPlanUseCases.GetAllRentalPlans;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.GetAllRentalPlans
{
    public class GetAllRentalPlansUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;

        public GetAllRentalPlansUseCase(IRentalPlanRepository rentalPlanRepository)
        {
            _rentalPlanRepository = rentalPlanRepository;
        }

        public async Task<GetAllRentalPlansOutput> ExecuteAsync(GetAllRentalPlansInput input, CancellationToken cancellationToken)
        {
            var rentalPlans = await _rentalPlanRepository.GetAllAsync();

            return new GetAllRentalPlansOutput
            {
                RentalPlans = rentalPlans.Select(rp => new GetAllRentalPlansOutput.RentalPlanDto
                {
                    Id = rp.Id,
                    Name = rp.Name,
                    DurationDays = rp.DurationDays,
                    CostPerDay = rp.CostPerDay
                }).ToList()
            };
        }
    }
}
