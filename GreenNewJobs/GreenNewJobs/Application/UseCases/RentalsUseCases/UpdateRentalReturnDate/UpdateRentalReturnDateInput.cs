namespace GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRentalReturnDate
{
    public class UpdateRentalReturnDateInput
    {
        public Guid RentalId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
