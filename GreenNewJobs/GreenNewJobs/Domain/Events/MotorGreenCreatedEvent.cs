namespace GreenNewJobs.Domain.Events
{
    public class MotorcycleGreenCreatedEvent
    {
        public Guid Id { get; }
        public string Model { get; }
        public int Year { get; }
        public string Plate { get; }
        public bool Available { get; }
        public DateTime CreatedAt { get; }

        public MotorcycleGreenCreatedEvent(Guid id, string model, int year, string plate, bool available, DateTime createdAt)
        {
            Id = id;
            Model = model;
            Year = year;
            Plate = plate;
            Available = available;
            CreatedAt = createdAt;
        }
    }
}
