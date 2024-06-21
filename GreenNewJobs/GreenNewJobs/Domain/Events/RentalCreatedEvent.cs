namespace GreenNewJobs.Domain.Events
{
    public class RentalCreatedEvent
    {
        public Guid Id { get; }
        public Guid DeliveryPersonId { get; }
        public Guid MotorcycleGreenId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateTime ExpectedEndDate { get; }
        public decimal Cost { get; }
        public DateTime CreatedAt { get; }

        public RentalCreatedEvent(Guid id, Guid driverId, Guid motorcycleGreenId, DateTime startDate, DateTime endDate, DateTime expectedEndDate, decimal cost, DateTime createdAt)
        {
            Id = id;
            DeliveryPersonId = driverId;
            MotorcycleGreenId = motorcycleGreenId;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
            Cost = cost;
            CreatedAt = createdAt;
        }
    }
}
