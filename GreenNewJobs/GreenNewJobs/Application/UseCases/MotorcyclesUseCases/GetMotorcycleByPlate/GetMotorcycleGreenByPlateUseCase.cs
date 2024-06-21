using GreenNewJobs.Domain.Interfaces;
using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotorcycleByPlate
{
    public class GetMotorcycleGreenByPlateUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;

        public GetMotorcycleGreenByPlateUseCase(IMotorcycleGreenRepository motorcycleGreenRepository)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
        }

        public async Task<Result<IEnumerable<GetMotorcycleGreenByPlateOutput>>> ExecuteAsync(GetMotorcycleGreenByPlateInput command, CancellationToken cancellationToken)
        {
            var motorcycles = await _motorcycleGreenRepository.GetByPlateAsync(command.Plate);

            if (motorcycles == null || !motorcycles.Any())
            {
                return Result<IEnumerable<GetMotorcycleGreenByPlateOutput>>.Failure(new List<ValidationFailure>
                {
                    new ValidationFailure("Plate", "No motorcycles found with the specified plate")
                });
            }

            var output = motorcycles.Select(m => GetMotorcycleGreenByPlateOutput.FromEntity(m)).ToList();
            return Result<IEnumerable<GetMotorcycleGreenByPlateOutput>>.SuccessResponse(output);
        }
    }
}
