using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Domain.Interfaces
{
    public interface IMotorcycleGreenRepository
    {
        Task<bool> PlateExistsAsync(string plate);
        Task AddAsync(MotorcycleGreen motorcycleGreen);
        Task<MotorcycleGreen> GetByIdAsync(Guid id);
        Task<IEnumerable<MotorcycleGreen>> GetAllAsync();
        Task<IEnumerable<MotorcycleGreen>> GetByPlateAsync(string plate);
        Task UpdateAsync(MotorcycleGreen motorcycleGreen);
        Task DeleteAsync(Guid id);
    }
}
