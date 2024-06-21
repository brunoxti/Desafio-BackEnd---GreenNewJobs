namespace GreenNewJobs.Application.UseCases.OrdersUseCases.CreateOrder
{
    public class CreateOrderOutput
    {
        public Guid OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Guid> NotifiedDeliveryPersonIds { get; set; }
        public string Message { get; set; }
    }
}
