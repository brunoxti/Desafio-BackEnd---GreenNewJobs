namespace GreenNewJobs.Application.UseCases.OrdersUseCases.UpdateOrder
{
    public class UpdateOrderOutput
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
