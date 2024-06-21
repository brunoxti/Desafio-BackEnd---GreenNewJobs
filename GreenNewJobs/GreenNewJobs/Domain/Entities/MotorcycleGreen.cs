namespace GreenNewJobs.Domain.Entities
{
    public class MotorcycleGreen : BaseEntity
    {
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Plate { get; private set; }
        public bool Available { get; private set; }

        private MotorcycleGreen() { }

        private MotorcycleGreen(string model, int year, string plate)
        {
            Model = model;
            Year = year;
            Plate = plate;
            Available = true;
        }

        public static MotorcycleGreen Create(string model, int year, string plate)
        {
            return new MotorcycleGreen(model, year, plate);
        }

        public void Update(string model, int year, string plate)
        {
            Model = model;
            Year = year;
            Plate = plate;
        }

        public void SetAvailability(bool available)
        {
            Available = available;
        }
    }
}
