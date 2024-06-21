namespace GreenNewJobs.Domain.Events
{
    public class OrderAcceptedEvent
    {
        public Guid OrderId { get; }
        public Guid DeliveryPersonId { get; }

        public OrderAcceptedEvent(Guid orderId, Guid driverId)
        {
            OrderId = orderId;
            DeliveryPersonId = driverId;
        }
    }

}
