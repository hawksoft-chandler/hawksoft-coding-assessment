using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HawkSoft.CodingAssessment.Data.Entities
{
    public class UserBusinessContactDataEntity
    {
        [BsonId] public string Id { get; set; }

        public IEnumerable<BusinessContactDataEntity> Contacts { get; set; }
    }

    public class BusinessContactDataEntity
    {
        [BsonId] public string Id { get; set; }

        [BsonElement("email")] public string EmailAddress { get; set; }

        [BsonElement("name")] public string Name { get; set; }

        [BsonElement("address")] public string Address { get; set; }
    }
}