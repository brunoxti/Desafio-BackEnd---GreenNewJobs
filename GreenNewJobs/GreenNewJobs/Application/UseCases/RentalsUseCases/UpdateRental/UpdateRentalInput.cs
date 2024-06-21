namespace GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRental
{
    public class UpdateRentalInput
    {
        public Guid Id { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public decimal Cost { get; set; }
    }
}
