namespace GreenNewJobs.Application.UseCases.OrdersUseCases.DeliverOrder
{
    public class DeliverOrderOutput
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public string Message { get; set; }
    }
}
