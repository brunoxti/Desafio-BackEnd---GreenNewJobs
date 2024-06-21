using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.GetAllOrders
{
    public class GetAllOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<IEnumerable<Order>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders == null || !orders.Any())
            {
                return Result<IEnumerable<Order>>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Orders", "No orders found")
                });
            }

            return Result<IEnumerable<Order>>.SuccessResponse(orders);
        }
    }
}
