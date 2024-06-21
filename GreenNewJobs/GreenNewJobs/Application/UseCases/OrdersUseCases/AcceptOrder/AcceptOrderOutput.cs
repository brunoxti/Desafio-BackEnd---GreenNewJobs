namespace GreenNewJobs.Application.UseCases.OrdersUseCases.AcceptOrder
{
    public class AcceptOrderOutput
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public string Message { get; set; }
    }
}
