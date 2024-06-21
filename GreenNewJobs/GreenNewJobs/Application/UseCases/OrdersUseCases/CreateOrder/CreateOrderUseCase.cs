using GreenNewJobs.Application.Services;
using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.CreateOrder
{
    public class CreateOrderUseCase : UseCaseBase<CreateOrderInput, CreateOrderOutput>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly NotificationService _notificationService;
        private readonly ILogger<CreateOrderUseCase> _logger;

        public CreateOrderUseCase(
            IOrderRepository orderRepository,
            IEventDispatcher eventDispatcher,
            NotificationService notificationService,
            ILogger<CreateOrderUseCase> logger)
        {
            _orderRepository = orderRepository;
            _eventDispatcher = eventDispatcher;
            _notificationService = notificationService;
            _logger = logger;
        }

        protected override async Task<Result<CreateOrderOutput>> ExecuteCoreAsync(CreateOrderInput command)
        {
            var order = Order.Create(command.CreationDate, command.Value);
            _logger.LogInformation("Creating order: {OrderId}", order.Id);

            await _orderRepository.AddAsync(order);

            var eligibleDeliveryPersonIds = await _notificationService.NotifyEligibleDriversAsync(order.Id);

            var orderCreatedEvent = new OrderCreatedEvent(order.Id, order.CreationDate, order.Value, order.Status, order.CreatedAt, eligibleDeliveryPersonIds);
            _logger.LogInformation("Dispatching OrderCreated event: {OrderId} to {DeliveryPersonCount} drivers", order.Id, eligibleDeliveryPersonIds.Count);

            await _eventDispatcher.Dispatch(orderCreatedEvent);

            var output = new CreateOrderOutput
            {
                OrderId = order.Id,
                CreationDate = order.CreationDate,
                Value = order.Value,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                NotifiedDeliveryPersonIds = eligibleDeliveryPersonIds,
                Message = "Order created successfully"
            };

            return Result<CreateOrderOutput>.SuccessResponse(output);
        }
    }
}
