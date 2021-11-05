using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Entities;
using HawkSoft.CodingAssessment.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HawkSoft.CodingAssessment.Data.Repositories
{
    public interface IBusinessContactRepository
    {
        public Task<IResult<IEnumerable<BusinessContact>>> GetUserContactsPaginated(
            string userId, int offset, int chunkSize);
    }

    public class BusinessContactRepository : IBusinessContactRepository
    {
        private readonly IUserContactMongoDataContext _dataContext;

        public BusinessContactRepository(IUserContactMongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IResult<IEnumerable<BusinessContact>>> GetUserContactsPaginated(
            string userId, int offset, int chunkSize)
        {
            IResult<IEnumerable<BusinessContact>> output;
            try
            {
                var queryableCollection = _dataContext.BusinessContacts.AsQueryable();

                var list = await queryableCollection
                    .Where(x => x.Id == userId)
                    .SelectMany(x => x.Contacts)
                    .Skip(offset)
                    .Take(chunkSize)
                    .Select(x => new BusinessContact()
                    {
                        Id = x.ContactId,
                        Name = x.Name,
                        EmailAddress = x.EmailAddress,
                        Address = x.Address
                    })
                    .ToListAsync();

                output = Result<IEnumerable<BusinessContact>>.SuccessResult(list);
            }
            catch (Exception ex)
            {
                output = Result<IEnumerable<BusinessContact>>.ExceptionResult(ex);
            }

            return output;
        }
    }
}