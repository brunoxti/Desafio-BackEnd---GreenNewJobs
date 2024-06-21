using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetAllDeliveryPersons
{
    public class GetAllDeliveryPersonsOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        public string CNHImagePath { get; set; }
        public bool HasActiveRental { get; set; }
        public bool HasAcceptedOrder { get; set; }
        public List<Guid> Notifications { get; set; }

        public static GetAllDeliveryPersonsOutput FromEntity(DeliveryPerson deliveryPerson)
        {
            return new GetAllDeliveryPersonsOutput
            {
                Id = deliveryPerson.Id,
                Name = deliveryPerson.Name,
                CNPJ = deliveryPerson.CNPJ,
                BirthDate = deliveryPerson.BirthDate,
                CNHNumber = deliveryPerson.CNHNumber,
                CNHType = deliveryPerson.CNHType,
                CNHImagePath = deliveryPerson.CNHImagePath,
                HasActiveRental = deliveryPerson.HasActiveRental,
                HasAcceptedOrder = deliveryPerson.HasAcceptedOrder,
                Notifications = new List<Guid>(deliveryPerson.Notifications)
            };
        }
    }
}
