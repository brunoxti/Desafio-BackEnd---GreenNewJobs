namespace GreenNewJobs.Domain.Entities
{
    public class OrderNotification
    {
        public string OrderId { get; set; }
        public string DeliveryPersonId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
