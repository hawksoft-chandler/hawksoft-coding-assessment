using HawkSoft.CodingAssessment.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HawkSoft.CodingAssessment.Data
{
    public interface IUserContactMongoDataContext
    {
        IMongoCollection<UserBusinessContactDataEntity> BusinessContacts { get; }
    }

    public class UserContactMongoDataContext : IUserContactMongoDataContext
    {
        public UserContactMongoDataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("UserContactMongo"));
            var database = client.GetDatabase("user-contacts");

            BusinessContacts = database.GetCollection<UserBusinessContactDataEntity>("user-business-contacts");
        }

        public IMongoCollection<UserBusinessContactDataEntity> BusinessContacts { get; }
    }
}