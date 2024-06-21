namespace GreenNewJobs.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task AddEventAsync<TEvent>(TEvent eventToStore) where TEvent : class;
    }
}
