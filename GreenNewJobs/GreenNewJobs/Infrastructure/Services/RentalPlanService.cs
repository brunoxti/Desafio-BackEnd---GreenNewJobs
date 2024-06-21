using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;

public class RentalPlanService
{
    private readonly IRentalPlanRepository _rentalPlanRepository;

    public RentalPlanService(IRentalPlanRepository rentalPlanRepository)
    {
        _rentalPlanRepository = rentalPlanRepository;
    }

    public async Task<List<RentalPlan>> GetAllRentalPlansAsync()
    {
        return await _rentalPlanRepository.GetAllAsync();
    }

    public async Task<RentalPlan> GetRentalPlanByIdAsync(Guid id)
    {
        return await _rentalPlanRepository.GetByIdAsync(id);
    }
}
