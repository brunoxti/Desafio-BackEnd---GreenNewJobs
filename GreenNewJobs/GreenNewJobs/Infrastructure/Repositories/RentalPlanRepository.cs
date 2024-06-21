using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using MongoDB.Driver;

public class RentalPlanRepository : IRentalPlanRepository
{
    private readonly IMongoCollection<RentalPlan> _collection;

    public RentalPlanRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<RentalPlan>("RentalPlans");
    }

    public async Task<List<RentalPlan>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<RentalPlan> GetByIdAsync(Guid id)
    {
        return await _collection.Find(plan => plan.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(RentalPlan rentalPlan) // Implementar isso
    {
        await _collection.InsertOneAsync(rentalPlan);
    }
}
