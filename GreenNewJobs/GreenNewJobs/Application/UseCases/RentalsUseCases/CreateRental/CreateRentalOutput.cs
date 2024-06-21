using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental
{
    public class CreateRentalOutput
    {
        public Guid Id { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public Guid MotorcycleGreenId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }

        public static CreateRentalOutput FromEntity(Rental rental)
        {
            return new CreateRentalOutput
            {
                Id = rental.Id,
                DeliveryPersonId = rental.DeliveryPersonId,
                MotorcycleGreenId = rental.MotorcycleGreenId,
                PlanId = rental.PlanId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                ExpectedEndDate = rental.ExpectedEndDate,
                Cost = rental.Cost,
                CreatedAt = rental.CreatedAt
            };
        }
    }
}
