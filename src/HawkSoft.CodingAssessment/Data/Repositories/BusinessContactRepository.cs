using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Data.Entities;
using MongoDB.Driver;

namespace HawkSoft.CodingAssessment.Data.Repositories
{
    public interface IBusinessContactRepository
    {
        Task<IEnumerable<UserBusinessContactDataEntity>> Get();
    }

    public class BusinessContactRepository : IBusinessContactRepository
    {
        private readonly IUserContactMongoDataContext _dataContext;

        public BusinessContactRepository(IUserContactMongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<UserBusinessContactDataEntity>> Get()
        {
            return await _dataContext.BusinessContacts.Find(x => true).ToListAsync();
        }
    }
}