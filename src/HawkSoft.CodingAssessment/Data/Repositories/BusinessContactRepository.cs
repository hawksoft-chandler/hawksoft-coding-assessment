using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Entities;
using HawkSoft.CodingAssessment.Models;
using HawkSoft.CodingAssessment.Models.Commands;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HawkSoft.CodingAssessment.Data.Repositories
{
    public interface IBusinessContactRepository
    {
        public Task<IResult<IEnumerable<BusinessContact>>> GetUserContactsPaginated(
            string userId, int offset, int chunkSize);

        Task<IResult> CreateUserContact(CreateUserContactCommand command);
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
                    .Select(x => new BusinessContact
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

        public async Task<IResult> CreateUserContact(CreateUserContactCommand command)
        {
            IResult output;
            try
            {
                var entity = new BusinessContactDataEntity
                {
                    ContactId = command.ContactId,
                    Name = command.Name,
                    EmailAddress = command.EmailAddress,
                    Address = command.Address
                };

                var filter = Builders<UserBusinessContactDataEntity>
                    .Filter.Eq(user => user.Id, command.UserId);
                var update = Builders<UserBusinessContactDataEntity>
                    .Update.Push(user => user.Contacts, entity);

                await _dataContext.BusinessContacts.FindOneAndUpdateAsync(filter, update);
                output = Result.SuccessResult();
            }
            catch (Exception ex)
            {
                output = Result.ExceptionResult(ex);
            }

            return output;
        }
    }
}