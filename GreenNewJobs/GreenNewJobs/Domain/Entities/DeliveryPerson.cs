
using MongoDB.Bson.Serialization.Attributes;

namespace GreenNewJobs.Domain.Entities
{
    public class DeliveryPerson : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; private set; }

        [BsonElement("CNPJ")]
        public string CNPJ { get; private set; }

        [BsonElement("BirthDate")]
        public DateTime BirthDate { get; private set; }

        [BsonElement("CNHNumber")]
        public string CNHNumber { get; private set; }

        [BsonElement("CNHType")]
        public string CNHType { get; private set; }

        [BsonElement("CNHImagePath")]
        public string CNHImagePath { get; private set; }

        [BsonElement("HasActiveRental")]
        public bool HasActiveRental { get; private set; }

        [BsonElement("HasAcceptedOrder")]
        public bool HasAcceptedOrder { get; private set; }

        [BsonElement("Notifications")]
        public List<Guid> Notifications { get; private set; }

        private DeliveryPerson()
        {
            Notifications = new List<Guid>();
        }

        private DeliveryPerson(string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, bool hasActiveRental)
        {
            Name = name;
            CNPJ = cnpj;
            BirthDate = birthDate;
            CNHNumber = cnhNumber;
            CNHType = cnhType;
            HasActiveRental = hasActiveRental;
            HasAcceptedOrder = false;
            Notifications = new List<Guid>();
        }

        public static DeliveryPerson Create(string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, bool hasActiveRental)
        {
            return new DeliveryPerson(name, cnpj, birthDate, cnhNumber, cnhType, hasActiveRental);
        }

        public void Update(string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, bool hasActiveRental)
        {
            Name = name;
            CNPJ = cnpj;
            BirthDate = birthDate;
            CNHNumber = cnhNumber;
            CNHType = cnhType;
            HasActiveRental = hasActiveRental;
        }

        public void UpdateCNHImagePath(string cnhImagePath)
        {
            CNHImagePath = cnhImagePath;
        }

        public bool IsLicensedForCategoryA()
        {
            return CNHType == "A" || CNHType.Contains("A");
        }

        public void Notify(Guid orderId)
        {
            Notifications.Add(orderId);
        }

        public void AcceptOrder()
        {
            HasAcceptedOrder = true;
        }

        public void CompleteOrder()
        {
            HasAcceptedOrder = false;
        }

        public void StartRental()
        {
            HasActiveRental = true;
        }

        public void EndRental()
        {
            HasActiveRental = false;
        }
    }
}
