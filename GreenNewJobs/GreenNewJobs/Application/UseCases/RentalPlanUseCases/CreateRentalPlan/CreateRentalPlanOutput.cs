namespace GreenNewJobs.Application.UseCases.RentalPlanUseCases.CreateRentalPlan
{
    public class CreateRentalPlanOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public decimal CostPerDay { get; set; }
    }
}
