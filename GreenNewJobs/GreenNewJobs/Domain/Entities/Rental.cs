namespace GreenNewJobs.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public Guid DeliveryPersonId { get; private set; }
        public Guid MotorcycleGreenId { get; private set; }
        public Guid PlanId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public decimal Cost { get; private set; }
        public decimal TotalCost { get; private set; }

        private Rental() { }

        private Rental(Guid driverId, Guid motorcycleGreenId, Guid planId, DateTime startDate, DateTime endDate, DateTime expectedEndDate, decimal cost)
        {
            DeliveryPersonId = driverId;
            MotorcycleGreenId = motorcycleGreenId;
            PlanId = planId;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
            Cost = cost;
            TotalCost = cost;
        }

        public static Rental Create(Guid driverId, Guid motorcycleGreenId, Guid planId, DateTime startDate, DateTime endDate, DateTime expectedEndDate, decimal cost)
        {
            return new Rental(driverId, motorcycleGreenId, planId, startDate, endDate, expectedEndDate, cost);
        }

        public void UpdateReturnDate(DateTime endDate, decimal totalCost)
        {
            EndDate = endDate;
            TotalCost = totalCost;
        }

        public void Update(DateTime endDate, DateTime expectedEndDate, decimal cost)
        {
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
            Cost = cost;
        }
    }
}
