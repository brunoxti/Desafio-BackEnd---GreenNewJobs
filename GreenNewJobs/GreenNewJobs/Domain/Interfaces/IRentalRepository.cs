using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IRentalRepository
    {
        Task AddAsync(Rental rental);
        Task<Rental> GetByIdAsync(Guid id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<IEnumerable<Rental>> GetRentalsByMotorcycleGreenIdAsync(Guid motorcycleGreenId);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Guid id);
    }
}
