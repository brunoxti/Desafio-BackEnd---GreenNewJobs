namespace GreenNewJobs.Domain.Events
{
    public class DeliveryPersonCreatedEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public string CNPJ { get; }
        public DateTime BirthDate { get; }
        public string CNHNumber { get; }
        public string CNHType { get; }
        public string CNHImage { get; }
        public DateTime CreatedAt { get; }

        public DeliveryPersonCreatedEvent(Guid id, string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, string cnhImage, DateTime createdAt)
        {
            Id = id;
            Name = name;
            CNPJ = cnpj;
            BirthDate = birthDate;
            CNHNumber = cnhNumber;
            CNHType = cnhType;
            CNHImage = cnhImage;
            CreatedAt = createdAt;
        }
    }
}
