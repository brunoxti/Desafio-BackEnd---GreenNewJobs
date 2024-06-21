using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.UpdateMotorcycle
{
    public class UpdateMotorcycleGreenOutput
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Plate { get; private set; }
        public bool Available { get; private set; }

        private UpdateMotorcycleGreenOutput(Guid id, string model, int year, string plate, bool available)
        {
            Id = id;
            Model = model;
            Year = year;
            Plate = plate;
            Available = available;
        }

        public static UpdateMotorcycleGreenOutput FromEntity(MotorcycleGreen motorcycleGreen)
        {
            return new UpdateMotorcycleGreenOutput(
                motorcycleGreen.Id,
                motorcycleGreen.Model,
                motorcycleGreen.Year,
                motorcycleGreen.Plate,
                motorcycleGreen.Available
            );
        }
    }
}
