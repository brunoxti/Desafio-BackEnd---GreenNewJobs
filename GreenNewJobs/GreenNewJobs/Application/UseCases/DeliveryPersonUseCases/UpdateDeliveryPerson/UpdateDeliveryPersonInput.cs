namespace GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.UpdateDeliveryPerson
{
    public class UpdateDeliveryPersonInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        public string CNHImagePath { get; set; }
    }
}
