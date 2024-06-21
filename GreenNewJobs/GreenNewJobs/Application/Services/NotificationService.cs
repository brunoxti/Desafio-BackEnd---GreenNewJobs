using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.Services
{
    public class NotificationService
    {
        private readonly IDeliveryPersonRepository _driverRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IDeliveryPersonRepository driverRepository, ILogger<NotificationService> logger)
        {
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<List<Guid>> NotifyEligibleDriversAsync(Guid orderId)
        {
            var eligibleDeliveryPersonIds = new List<Guid>();
            var drivers = await _driverRepository.GetAllWithActiveRentalAsync();
            _logger.LogInformation("Found {DeliveryPersonCount} drivers with active rental", drivers.Count());

            foreach (var driver in drivers)
            {
                if (!await _driverRepository.HasAcceptedOrderAsync(driver.Id))
                {
                    eligibleDeliveryPersonIds.Add(driver.Id);
                    driver.Notify(orderId);
                    await _driverRepository.UpdateAsync(driver);
                    _logger.LogInformation("Notifying driver: {DeliveryPersonId} about order: {OrderId}", driver.Id, orderId);
                }
                else
                {
                    _logger.LogInformation("DeliveryPerson: {DeliveryPersonId} already has an accepted order, will not be notified", driver.Id);
                }
            }

            return eligibleDeliveryPersonIds;
        }
    }
}
