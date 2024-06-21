namespace GreenNewJobs.Application.UseCases.RentalPlanUseCases.CreateRentalPlan
{
    public class CreateRentalPlanInput
    {
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public decimal CostPerDay { get; set; }
    }
}
