using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Infrastructure.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GreenNewJobs.Infrastructure.Repositories
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly IMongoCollection<DeliveryPerson> _drivers;

        public DeliveryPersonRepository(MongoDbContext context)
        {
            _drivers = context.DeliveryPersons;
        }

        public async Task AddAsync(DeliveryPerson driver)
        {
            await _drivers.InsertOneAsync(driver);
        }

        public async Task<DeliveryPerson> GetByIdAsync(Guid id)
        {
            return await _drivers.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllAsync()
        {
            return await _drivers.Find(d => true).ToListAsync();
        }

        public async Task UpdateAsync(DeliveryPerson driver)
        {
            var filter = Builders<DeliveryPerson>.Filter.Eq(d => d.Id, driver.Id);
            await _drivers.ReplaceOneAsync(filter, driver);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _drivers.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<bool> ExistsByCNPJAsync(string cnpj)
        {
            return await _drivers.Find(d => d.CNPJ == cnpj).AnyAsync();
        }

        public async Task<bool> ExistsByCNHAsync(string cnhNumber)
        {
            return await _drivers.Find(d => d.CNHNumber == cnhNumber).AnyAsync();
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllWithActiveRentalAsync()
        {
            var filter = Builders<DeliveryPerson>.Filter.Eq(d => d.HasActiveRental, true);
            return await _drivers.Find(filter).ToListAsync();
        }

        public async Task<bool> HasAcceptedOrderAsync(Guid driverId)
        {
            var filter = Builders<DeliveryPerson>.Filter.And(
                Builders<DeliveryPerson>.Filter.Eq(d => d.Id, driverId),
                Builders<DeliveryPerson>.Filter.Eq(d => d.HasAcceptedOrder, true)
            );
            return await _drivers.Find(filter).AnyAsync();
        }

        public async Task AddNotificationAsync(Guid driverId, Guid orderId)
        {
            var filter = Builders<DeliveryPerson>.Filter.Eq(d => d.Id, driverId);
            var update = Builders<DeliveryPerson>.Update.Push(d => d.Notifications, orderId);
            await _drivers.UpdateOneAsync(filter, update);
        }

        public async Task<IEnumerable<DeliveryPerson>> GetDeliveryPersonsNotifiedForOrderAsync(Guid orderId)
        {
            var filter = new BsonDocument("Notifications", orderId);
            return await _drivers.Find(filter).ToListAsync();
        }

        public async Task SetAcceptedOrderAsync(Guid driverId)
        {
            var filter = Builders<DeliveryPerson>.Filter.Eq(d => d.Id, driverId);
            var update = Builders<DeliveryPerson>.Update.Set(d => d.HasAcceptedOrder, true);
            await _drivers.UpdateOneAsync(filter, update);
        }
    }
}
