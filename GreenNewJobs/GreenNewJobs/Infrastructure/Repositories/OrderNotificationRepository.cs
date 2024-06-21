using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Infrastructure.Context;
using MongoDB.Driver;

namespace GreenNewJobs.Infrastructure.Repositories
{
    public class OrderNotificationRepository : IOrderNotificationRepository
    {
        private readonly MongoDbContext _context;

        public OrderNotificationRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task SaveNotificationAsync(OrderNotification notification)
        {
            await _context.OrderNotifications.InsertOneAsync(notification);
        }

        public async Task<IEnumerable<OrderNotification>> GetAllNotificationsAsync()
        {
            return await _context.OrderNotifications.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<OrderNotification>> GetNotificationsByOrderIdAsync(string orderId)
        {
            return await _context.OrderNotifications.Find(n => n.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<OrderNotification>> GetByOrderIdAsync(string orderId)
        {
            return await _context.OrderNotifications.Find(on => on.OrderId == orderId).ToListAsync();
        }
    }
}
