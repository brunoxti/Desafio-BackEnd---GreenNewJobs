using GreenNewJobs.Domain.Events;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.AcceptOrder
{
    public class AcceptOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<AcceptOrderUseCase> _logger;

        public AcceptOrderUseCase(IOrderRepository orderRepository, IDeliveryPersonRepository driverRepository, IEventDispatcher eventDispatcher, ILogger<AcceptOrderUseCase> logger)
        {
            _orderRepository = orderRepository;
            _driverRepository = driverRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task<Result<AcceptOrderOutput>> ExecuteAsync(AcceptOrderInput command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);
            if (order == null || order.Status != Domain.OrderStatus.Available)
            {
                _logger.LogError("Order not found or not available: {OrderId}", command.OrderId);
                return Result<AcceptOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("OrderId", "Order not found or not available")
                });
            }

            var driver = await _driverRepository.GetByIdAsync(command.DeliveryPersonId);
            if (driver == null || !driver.HasActiveRental)
            {
                _logger.LogError("DeliveryPerson cannot accept the order: {DeliveryPersonId}", command.DeliveryPersonId);
                return Result<AcceptOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersonId", "DeliveryPerson cannot accept the order")
                });
            }

            if (!driver.Notifications.Contains(command.OrderId))
            {
                _logger.LogError("DeliveryPerson was not notified about the order: {DeliveryPersonId}", command.DeliveryPersonId);
                return Result<AcceptOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersonId", "DeliveryPerson was not notified about the order")
                });
            }

            order.UpdateStatus(Domain.OrderStatus.Accepted);
            await _orderRepository.UpdateAsync(order);

            driver.AcceptOrder();
            await _driverRepository.UpdateAsync(driver);

            var orderAcceptedEvent = new OrderAcceptedEvent(order.Id, driver.Id);
            _logger.LogInformation("Order accepted: {OrderId} by driver: {DeliveryPersonId}", order.Id, driver.Id);

            await _eventDispatcher.Dispatch(orderAcceptedEvent);

            var output = new AcceptOrderOutput
            {
                OrderId = order.Id,
                DeliveryPersonId = driver.Id,
                Message = "Order accepted successfully"
            };

            return Result<AcceptOrderOutput>.SuccessResponse(output);
        }
    }
}
