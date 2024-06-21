using GreenNewJobs.Domain;

namespace GreenNewJobs.Application.UseCases.OrdersUseCases.UpdateOrder
{
    public class UpdateOrderInput
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Value { get; set; }
        public OrderStatus Status { get; set; }
    }
}
