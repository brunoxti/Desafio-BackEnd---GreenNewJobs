using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Infrastructure.Context;
using MongoDB.Driver;

namespace GreenNewJobs.Infrastructure.Repositories
{
    public class MotorcycleGreenRepository : IMotorcycleGreenRepository
    {
        private readonly IMongoCollection<MotorcycleGreen> _motorcycleGreens;

        public MotorcycleGreenRepository(MongoDbContext context)
        {
            _motorcycleGreens = context.MotorcycleGreens;
        }

        public async Task<bool> PlateExistsAsync(string plate)
        {
            var filter = Builders<MotorcycleGreen>.Filter.Eq(m => m.Plate, plate);
            var count = await _motorcycleGreens.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task AddAsync(MotorcycleGreen motorcycleGreen)
        {
            await _motorcycleGreens.InsertOneAsync(motorcycleGreen);
        }

        public async Task<MotorcycleGreen> GetByIdAsync(Guid id)
        {
            return await _motorcycleGreens.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MotorcycleGreen>> GetAllAsync()
        {
            return await _motorcycleGreens.Find(m => true).ToListAsync();
        }

        public async Task<IEnumerable<MotorcycleGreen>> GetByPlateAsync(string plate)
        {
            return await _motorcycleGreens.Find(m => m.Plate == plate).ToListAsync();
        }

        public async Task UpdateAsync(MotorcycleGreen motorcycleGreen)
        {
            await _motorcycleGreens.ReplaceOneAsync(m => m.Id == motorcycleGreen.Id, motorcycleGreen);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _motorcycleGreens.DeleteOneAsync(m => m.Id == id);
        }
    }
}
