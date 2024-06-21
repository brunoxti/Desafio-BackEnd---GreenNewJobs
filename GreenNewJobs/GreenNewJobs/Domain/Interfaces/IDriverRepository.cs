using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IDeliveryPersonRepository
    {
        Task AddAsync(DeliveryPerson driver);
        Task<DeliveryPerson> GetByIdAsync(Guid id);
        Task<IEnumerable<DeliveryPerson>> GetAllAsync();
        Task UpdateAsync(DeliveryPerson driver);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsByCNPJAsync(string cnpj);
        Task<bool> ExistsByCNHAsync(string cnhNumber);
        Task<IEnumerable<DeliveryPerson>> GetAllWithActiveRentalAsync();
        Task<bool> HasAcceptedOrderAsync(Guid driverId);
        Task AddNotificationAsync(Guid driverId, Guid orderId);
        Task<IEnumerable<DeliveryPerson>> GetDeliveryPersonsNotifiedForOrderAsync(Guid orderId);
    }
}
