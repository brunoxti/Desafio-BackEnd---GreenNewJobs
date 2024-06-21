namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson
{
    public class CreateDeliveryPersonOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        public string CNHImagePath { get; set; }

        public static CreateDeliveryPersonOutput FromEntity(Domain.Entities.DeliveryPerson deliveryPerson)
        {
            return new CreateDeliveryPersonOutput
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
