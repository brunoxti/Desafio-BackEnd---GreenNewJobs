using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Infrastructure.Context;
using MongoDB.Driver;

namespace GreenNewJobs.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentals;

        public RentalRepository(MongoDbContext context)
        {
            _rentals = context.Rentals;
        }

        public async Task AddAsync(Rental rental)
        {
            await _rentals.InsertOneAsync(rental);
        }

        public async Task<Rental> GetByIdAsync(Guid id)
        {
            return await _rentals.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _rentals.Find(r => true).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsByMotorcycleGreenIdAsync(Guid motorcycleGreenId)
        {
            return await _rentals.Find(r => r.MotorcycleGreenId == motorcycleGreenId).ToListAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            await _rentals.ReplaceOneAsync(r => r.Id == rental.Id, rental);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _rentals.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rental>> GetByMotorcycleGreenIdAsync(Guid motorcycleGreenId)
        {
            var filter = Builders<Rental>.Filter.Eq(r => r.MotorcycleGreenId, motorcycleGreenId);
            return await _rentals.Find(filter).ToListAsync();
        }
    }
}
