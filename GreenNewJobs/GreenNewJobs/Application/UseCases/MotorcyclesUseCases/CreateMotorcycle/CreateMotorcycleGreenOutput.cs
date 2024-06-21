using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.CreateMotorcycle
{
    public class CreateMotorcycleGreenOutput
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Plate { get; private set; }
        public bool Available { get; private set; }

        private CreateMotorcycleGreenOutput(Guid id, string model, int year, string plate, bool available)
        {
            Id = id;
            Model = model;
            Year = year;
            Plate = plate;
            Available = available;
        }

        public static CreateMotorcycleGreenOutput FromEntity(MotorcycleGreen motorcycleGreen)
        {
            return new CreateMotorcycleGreenOutput(
                motorcycleGreen.Id,
                motorcycleGreen.Model,
                motorcycleGreen.Year,
                motorcycleGreen.Plate,
                motorcycleGreen.Available
            );
        }
    }
}
