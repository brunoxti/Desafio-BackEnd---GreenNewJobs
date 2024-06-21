namespace GreenNewJobs.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreationDate { get; private set; }
        public decimal Value { get; private set; }
        public OrderStatus Status { get; private set; }

        private Order() { }

        private Order(DateTime creationDate, decimal value)
        {
            CreationDate = creationDate;
            Value = value;
            Status = OrderStatus.Available;
        }

        public static Order Create(DateTime creationDate, decimal value)
        {
            return new Order(creationDate, value);
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }

        public static bool IsValidStatus(OrderStatus status)
        {
            return Enum.IsDefined(typeof(OrderStatus), status);
        }
    }
}
