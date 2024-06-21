using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.RentalPlanUseCases.CreateRentalPlan
{
    public class CreateRentalPlanUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;

        public CreateRentalPlanUseCase(IRentalPlanRepository rentalPlanRepository)
        {
            _rentalPlanRepository = rentalPlanRepository;
        }

        public async Task<CreateRentalPlanOutput> ExecuteAsync(CreateRentalPlanInput input, CancellationToken cancellationToken)
        {
            var rentalPlan = new RentalPlan(input.Name, input.DurationDays, input.CostPerDay);
            await _rentalPlanRepository.AddAsync(rentalPlan);

            return new CreateRentalPlanOutput
            {
                Id = rentalPlan.Id,
                Name = rentalPlan.Name,
                DurationDays = rentalPlan.DurationDays,
                CostPerDay = rentalPlan.CostPerDay
            };
        }
    }
}
