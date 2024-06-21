namespace GreenNewJobs.Application.UseCases.OrdersUseCases.DeliverOrder
{
    public class DeliverOrderInput
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryPersonId { get; set; }

        public DeliverOrderInput() { }

        public DeliverOrderInput(Guid orderId, Guid deliveryPersonId)
        {
            OrderId = orderId;
            DeliveryPersonId = deliveryPersonId;
        }
    }
}
