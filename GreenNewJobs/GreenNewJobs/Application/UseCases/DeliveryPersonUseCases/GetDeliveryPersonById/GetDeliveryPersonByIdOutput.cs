using GreenNewJobs.Domain.Entities;

namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetDeliveryPersonById
{
    public class GetDeliveryPersonByIdOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        public string CNHImagePath { get; set; }

        public static GetDeliveryPersonByIdOutput FromEntity(DeliveryPerson deliveryPerson)
        {
            return new GetDeliveryPersonByIdOutput
            {
                Id = deliveryPerson.Id,
                Name = deliveryPerson.Name,
                CNPJ = deliveryPerson.CNPJ,
                BirthDate = deliveryPerson.BirthDate,
                CNHNumber = deliveryPerson.CNHNumber,
                CNHType = deliveryPerson.CNHType,
                CNHImagePath = deliveryPerson.CNHImagePath
            };
        }
    }
}
