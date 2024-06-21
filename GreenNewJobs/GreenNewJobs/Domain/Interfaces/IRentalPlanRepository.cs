using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IRentalPlanRepository
    {
        Task<List<RentalPlan>> GetAllAsync();
        Task<RentalPlan> GetByIdAsync(Guid id);
        Task AddAsync(RentalPlan rentalPlan);
    }
}
