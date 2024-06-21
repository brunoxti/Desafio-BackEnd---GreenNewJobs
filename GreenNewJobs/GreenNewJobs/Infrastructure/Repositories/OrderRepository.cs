using MongoDB.Driver;
using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Domain.Events;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;
    private readonly IMongoCollection<OrderCreatedEvent> _notifications;

    public OrderRepository(IMongoDatabase database)
    {
        _orders = database.GetCollection<Order>("Orders");
        _notifications = database.GetCollection<OrderCreatedEvent>("OrderNotifications");
    }

    public async Task AddAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _orders.Find(o => true).ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
    }

    public async Task AddNotificationAsync(OrderCreatedEvent notification)
    {
        await _notifications.InsertOneAsync(notification);
    }
}
