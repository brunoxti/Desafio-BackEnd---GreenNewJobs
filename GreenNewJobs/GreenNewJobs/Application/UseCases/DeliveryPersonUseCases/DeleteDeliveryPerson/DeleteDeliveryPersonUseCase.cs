using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.DeleteDeliveryPerson
{
    public class DeleteDeliveryPersonUseCase
    {
        private readonly IDeliveryPersonRepository _driverRepository;

        public DeleteDeliveryPersonUseCase(IDeliveryPersonRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task ExecuteAsync(DeleteDeliveryPersonInput command, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(command.Id);

            if (driver == null)
            {
                throw new InvalidOperationException("DeliveryPerson not found.");
            }

            await _driverRepository.DeleteAsync(command.Id);
        }
    }
}
