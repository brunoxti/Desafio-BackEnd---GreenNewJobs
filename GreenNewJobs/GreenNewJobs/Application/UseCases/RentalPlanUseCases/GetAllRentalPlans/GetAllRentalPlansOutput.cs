namespace GreenNewJobs.Application.UseCases.GetAllRentalPlans
{
    public class GetAllRentalPlansOutput
    {
        public List<RentalPlanDto> RentalPlans { get; set; }

        public class RentalPlanDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int DurationDays { get; set; }
            public decimal CostPerDay { get; set; }
        }
    }
}
