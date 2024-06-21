using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Events;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task UpdateAsync(Order order);
        Task AddNotificationAsync(OrderCreatedEvent notification);
    }

}
