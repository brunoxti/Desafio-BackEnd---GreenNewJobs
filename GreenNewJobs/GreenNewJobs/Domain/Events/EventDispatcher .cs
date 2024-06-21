namespace GreenNewJobs.Domain.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<Func<object, Task>>> _handlers;

        public EventDispatcher()
        {
            _handlers = new Dictionary<Type, List<Func<object, Task>>>();
        }

        public void RegisterUseCase<TEvent>(Func<TEvent, Task> handler) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Func<object, Task>>();
            }

            _handlers[eventType].Add((e) => handler((TEvent)e));
        }

        public async Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (_handlers.ContainsKey(eventType))
            {
                var handlers = _handlers[eventType];
                foreach (var handler in handlers)
                {
                    await handler(eventToDispatch);
                }
            }
        }
    }
}
