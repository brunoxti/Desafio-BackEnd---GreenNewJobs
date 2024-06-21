using GreenNewJobs.Domain.Interfaces;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetNotifiedDeliveryPersons
{
    public class GetNotifiedDeliveryPersonsUseCase
    {
        private readonly IOrderNotificationRepository _orderNotificationRepository;

        public GetNotifiedDeliveryPersonsUseCase(IOrderNotificationRepository orderNotificationRepository)
        {
            _orderNotificationRepository = orderNotificationRepository;
        }

        public async Task<IEnumerable<string>> ExecuteAsync(string orderId)
        {
            var notifications = await _orderNotificationRepository.GetByOrderIdAsync(orderId);
            return notifications.Select(n => n.DeliveryPersonId);
        }
    }
}
