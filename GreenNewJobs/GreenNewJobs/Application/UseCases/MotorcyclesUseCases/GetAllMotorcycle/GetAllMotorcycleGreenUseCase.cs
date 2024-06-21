using GreenNewJobs.Domain.Entities;
using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetAllMotorcycle
{
    public class GetAllMotorcycleGreenUseCase
    {
        private readonly IMotorcycleGreenRepository _motorcycleGreenRepository;

        public GetAllMotorcycleGreenUseCase(IMotorcycleGreenRepository motorcycleGreenRepository)
        {
            _motorcycleGreenRepository = motorcycleGreenRepository;
        }

        public async Task<Result<IEnumerable<MotorcycleGreen>>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var motorcycleGreens = await _motorcycleGreenRepository.GetAllAsync();

            if (motorcycleGreens == null || !motorcycleGreens.Any())
            {
                return Result<IEnumerable<MotorcycleGreen>>.Failure(new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("Motorcycles", "No motorcycles found")
                });
            }

            return Result<IEnumerable<MotorcycleGreen>>.SuccessResponse(motorcycleGreens);
        }
    }
}
