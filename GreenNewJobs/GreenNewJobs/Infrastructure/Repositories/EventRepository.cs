using System.Text.Json;
using GreenNewJobs.Domain.Interfaces;
using MongoDB.Driver;

namespace GreenNewJobs.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _events;

        public EventRepository(IMongoDatabase database)
        {
            _events = database.GetCollection<Event>("Events");
        }

        public async Task AddEventAsync<TEvent>(TEvent eventToStore) where TEvent : class
        {
            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
                EventType = eventToStore.GetType().Name,
                EventData = JsonSerializer.Serialize(eventToStore),
                CreatedAt = DateTime.UtcNow
            };

            await _events.InsertOneAsync(eventEntity);
        }
    }

    public class Event
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
