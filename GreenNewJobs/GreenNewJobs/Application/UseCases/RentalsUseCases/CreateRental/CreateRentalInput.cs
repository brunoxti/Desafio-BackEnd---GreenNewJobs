namespace GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental
{
    public class CreateRentalInput
    {
        public Guid DeliveryPersonId { get; set; }
        public Guid MotorcycleGreenId { get; set; }
        public Guid PlanId { get; set; }
    }
}
