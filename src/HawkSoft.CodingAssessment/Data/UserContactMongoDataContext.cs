using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HawkSoft.CodingAssessment.Data
{
    public interface IUserContactMongoDataContext
    {
        IMongoCollection<UserBusinessContactDataEntity> BusinessContacts { get; }
        Task<IClientSessionHandle> StartSessionAsync();
    }

    public class UserContactMongoDataContext : IUserContactMongoDataContext
    {
        private IMongoClient _client;
        public UserContactMongoDataContext(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("UserContactMongo"));
            var database = _client.GetDatabase("user-contacts");

            BusinessContacts = database.GetCollection<UserBusinessContactDataEntity>("user-business-contacts");
        }

        public IMongoCollection<UserBusinessContactDataEntity> BusinessContacts { get; }
         public async Task<IClientSessionHandle> StartSessionAsync() => await _client.StartSessionAsync();
    }
}