using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.UpdateOrder
{
    public class UpdateOrderUseCase : UseCaseBase<UpdateOrderInput, UpdateOrderOutput>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        protected override async Task<Result<UpdateOrderOutput>> ExecuteCoreAsync(UpdateOrderInput command)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);
            if (order == null)
            {
                return Result<UpdateOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Id", "Order not found")
                });
            }

            if (!Order.IsValidStatus(command.Status))
            {
                return Result<UpdateOrderOutput>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Status", "Invalid status")
                });
            }

            order.UpdateStatus(command.Status);
            await _orderRepository.UpdateAsync(order);

            var output = new UpdateOrderOutput
            {
                OrderId = order.Id,
                Status = order.Status.ToString(),
                Message = "Order updated successfully"
            };

            return Result<UpdateOrderOutput>.SuccessResponse(output);
        }
    }
}
