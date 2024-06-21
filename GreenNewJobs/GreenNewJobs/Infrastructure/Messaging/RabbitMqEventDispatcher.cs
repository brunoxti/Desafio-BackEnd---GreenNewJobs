using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Infrastructure.Messaging
{
    public class RabbitMqEventDispatcher : IEventDispatcher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IEventRepository _eventRepository;

        public RabbitMqEventDispatcher(IConnection connection, IEventRepository eventRepository)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _eventRepository = eventRepository;
        }

        public async Task Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : class
        {
            var eventName = eventToDispatch.GetType().Name;
            var message = JsonSerializer.Serialize(eventToDispatch);
            var body = Encoding.UTF8.GetBytes(message);

            await _eventRepository.AddEventAsync(eventToDispatch);

            if (eventName == nameof(OrderCreatedEvent) || eventName == nameof(OrderAcceptedEvent))
            {
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: "order_notifications",
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
