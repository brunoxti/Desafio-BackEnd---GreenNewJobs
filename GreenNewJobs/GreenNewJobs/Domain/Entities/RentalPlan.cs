namespace GreenNewJobs.Domain.Entities
{
    public class RentalPlan : BaseEntity
    {
        public string Name { get; private set; }
        public int DurationDays { get; private set; }
        public decimal CostPerDay { get; private set; }

        private RentalPlan() { }

        public RentalPlan(string name, int durationDays, decimal costPerDay)
        {
            Name = name;
            DurationDays = durationDays;
            CostPerDay = costPerDay;
        }
    }
}
