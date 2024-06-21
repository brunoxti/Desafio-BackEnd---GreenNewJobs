namespace GreenNewJobs.Application.UseCases.OrdersUseCases.AcceptOrder
{
    public class AcceptOrderInput
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryPersonId { get; set; }

        public AcceptOrderInput() { }

        public AcceptOrderInput(Guid orderId, Guid deliveryPersonId)
        {
            OrderId = orderId;
            DeliveryPersonId = deliveryPersonId;
        }
    }
}
