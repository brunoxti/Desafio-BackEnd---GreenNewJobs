using MongoDB.Driver;
using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<MotorcycleGreen> MotorcycleGreens => _database.GetCollection<MotorcycleGreen>("MotorcycleGreens");
        public IMongoCollection<DeliveryPerson> DeliveryPersons => _database.GetCollection<DeliveryPerson>("DeliveryPersons");
        public IMongoCollection<Rental> Rentals => _database.GetCollection<Rental>("Rentals");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<OrderNotification> OrderNotifications => _database.GetCollection<OrderNotification>("OrderNotifications");
    }
}
