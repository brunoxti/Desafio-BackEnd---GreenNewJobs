using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Domain.Events;

public class OrderNotificationConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OrderNotificationConsumer> _logger;
    private readonly IModel _channel;

    public OrderNotificationConsumer(IServiceScopeFactory serviceScopeFactory, ILogger<OrderNotificationConsumer> logger, IConnection connection)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _channel = connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel.QueueDeclare(queue: "order_notifications",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("REceived message: {Message}", message);

            var notification = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

            if (notification != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                    await orderRepository.AddNotificationAsync(notification);
                    _logger.LogInformation("Notification process: {Id}", notification.Id);
                }
            }
            else
            {
                _logger.LogWarning("Failure to deserialze a message: {Message}", message);
            }
        };

        _channel.BasicConsume(queue: "order_notifications",
                              autoAck: true,
                              consumer: consumer);

        return Task.CompletedTask;
    }
}
