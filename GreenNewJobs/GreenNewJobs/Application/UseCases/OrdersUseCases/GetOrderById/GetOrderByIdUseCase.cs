using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.GetOrderById
{
    public class GetOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<Order>> ExecuteAsync(GetOrderByIdInput command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);

            if (order == null)
            {
                return Result<Order>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Order not found")
                });
            }

            return Result<Order>.SuccessResponse(order);
        }
    }
}
