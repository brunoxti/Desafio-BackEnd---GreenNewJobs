using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Domain;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.DeliverOrder
{
    public class DeliverOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly ILogger<DeliverOrderUseCase> _logger;

        public DeliverOrderUseCase(IOrderRepository orderRepository, IDeliveryPersonRepository driverRepository, ILogger<DeliverOrderUseCase> logger)
        {
            _orderRepository = orderRepository;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Result<DeliverOrderOutput>> ExecuteAsync(DeliverOrderInput command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);
            if (order == null || order.Status != OrderStatus.Accepted)
            {
                _logger.LogError("Order not found or not accepted: {OrderId}", command.OrderId);
                return Result<DeliverOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("OrderId", "Order not found or not accepted")
                });
            }

            var driver = await _driverRepository.GetByIdAsync(command.DeliveryPersonId);
            if (driver == null || !driver.HasActiveRental || !driver.HasAcceptedOrder)
            {
                _logger.LogError("DeliveryPerson cannot deliver the order: {DeliveryPersonId}", command.DeliveryPersonId);
                return Result<DeliverOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersonId", "DeliveryPerson cannot deliver the order")
                });
            }

            if (!driver.Notifications.Contains(command.OrderId))
            {
                _logger.LogError("DeliveryPerson was not notified about the order: {DeliveryPersonId}", command.DeliveryPersonId);
                return Result<DeliverOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("DeliveryPersonId", "DeliveryPerson was not notified about the order")
                });
            }

            order.UpdateStatus(OrderStatus.Delivered);
            await _orderRepository.UpdateAsync(order);

            driver.CompleteOrder();
            await _driverRepository.UpdateAsync(driver);

            _logger.LogInformation("Order delivered: {OrderId} by driver: {DeliveryPersonId}", order.Id, driver.Id);

            var output = new DeliverOrderOutput
            {
                OrderId = order.Id,
                DeliveryPersonId = driver.Id,
                Message = "Order delivered successfully"
            };

            return Result<DeliverOrderOutput>.SuccessResponse(output);
        }
    }
}
