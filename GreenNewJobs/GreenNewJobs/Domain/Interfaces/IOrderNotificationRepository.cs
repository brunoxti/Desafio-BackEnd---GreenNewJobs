using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IOrderNotificationRepository
    {
        Task SaveNotificationAsync(OrderNotification notification);
        Task<IEnumerable<OrderNotification>> GetAllNotificationsAsync();
        Task<IEnumerable<OrderNotification>> GetNotificationsByOrderIdAsync(string orderId);
        Task<IEnumerable<OrderNotification>> GetByOrderIdAsync(string orderId);
    }
}
