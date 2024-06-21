using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GreenNewJobs.Domain.Events
{
    public class OrderCreatedEvent
    {
        [BsonId]
        public Guid Id { get; private set; }

        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; private set; }

        [BsonElement("Value")]
        public decimal Value { get; private set; }

        [BsonElement("Status")]
        public OrderStatus Status { get; private set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; private set; }

        [BsonElement("EligibleDeliveryPersonIds")]
        public List<Guid> EligibleDeliveryPersonIds { get; private set; }

        public OrderCreatedEvent(Guid id, DateTime creationDate, decimal value, OrderStatus status, DateTime createdAt, List<Guid> eligibleDeliveryPersonIds)
        {
            Id = id;
            CreationDate = creationDate;
            Value = value;
            Status = status;
            CreatedAt = createdAt;
            EligibleDeliveryPersonIds = eligibleDeliveryPersonIds;
        }
    }
}
