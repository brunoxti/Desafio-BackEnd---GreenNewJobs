using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.DeleteMotocycle
{
    public class DeleteMotorcycleGreenOutput
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public bool Available { get; set; }

        public static DeleteMotorcycleGreenOutput FromEntity(MotorcycleGreen motorcycleGreen)
        {
            return new DeleteMotorcycleGreenOutput
            {
                Id = motorcycleGreen.Id,
                Model = motorcycleGreen.Model,
                Year = motorcycleGreen.Year,
                Plate = motorcycleGreen.Plate,
                Available = motorcycleGreen.Available
            };
        }
    }
}
