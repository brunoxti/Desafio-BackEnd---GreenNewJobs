namespace GreenNewJobs.Domain.Events
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : class;
    }
}
