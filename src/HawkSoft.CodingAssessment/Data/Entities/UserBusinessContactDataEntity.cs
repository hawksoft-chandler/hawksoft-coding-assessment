using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HawkSoft.CodingAssessment.Data.Entities
{
    public class UserBusinessContactDataEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("contacts")]
        public IEnumerable<BusinessContactDataEntity> Contacts { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class BusinessContactDataEntity
    {
        [BsonElement("contact-id")]
        public string ContactId { get; set; }

        [BsonElement("email")]
        public string EmailAddress { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }
    }
}