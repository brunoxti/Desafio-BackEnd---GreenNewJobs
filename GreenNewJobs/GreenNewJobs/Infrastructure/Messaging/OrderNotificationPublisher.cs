using GreenNewJobs.Domain.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GreenNewJobs.Infrastructure.Messaging
{
    public class OrderNotificationPublisher
    {
        private readonly IConnection _connection;

        public OrderNotificationPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public void PublishOrderNotification(OrderNotification notification)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "order_notifications", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(notification);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "order_notifications", basicProperties: null, body: body);
        }
    }
}
