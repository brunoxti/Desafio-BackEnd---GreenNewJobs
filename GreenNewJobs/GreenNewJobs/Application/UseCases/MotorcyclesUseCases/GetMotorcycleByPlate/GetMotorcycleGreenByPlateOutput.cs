using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotorcycleByPlate
{
    public class GetMotorcycleGreenByPlateOutput
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public bool Available { get; set; }

        public static GetMotorcycleGreenByPlateOutput FromEntity(MotorcycleGreen motorcycleGreen)
        {
            return new GetMotorcycleGreenByPlateOutput
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
