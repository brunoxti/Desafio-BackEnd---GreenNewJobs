using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotocycleById
{
    public class GetMotorcycleGreenByIdOutput
    {
        public Guid Id { get; set; }

        private GetMotorcycleGreenByIdOutput(Guid id)
        {
            Id = id;
        }

        public static GetMotorcycleGreenByIdOutput FromEntity(MotorcycleGreen motorcycleGreen)
        {
            return new GetMotorcycleGreenByIdOutput(
                motorcycleGreen.Id
            );
        }
    }
}
