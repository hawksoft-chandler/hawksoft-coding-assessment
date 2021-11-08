using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Entities;
using HawkSoft.CodingAssessment.Models;
using HawkSoft.CodingAssessment.Models.Commands;
using HawkSoft.CodingAssessment.Models.Queries;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HawkSoft.CodingAssessment.Data.Repositories
{
    public interface IBusinessContactRepository
    {
        Task<IResult<IEnumerable<BusinessContact>>> GetUserContactsPaginated(
            GetUserContactsPaginatedQuery query);

        Task<IResult> CreateUserContact(CreateUserContactCommand command);
        Task<IResult> UpdateUserBusinessContact(UpdateUserContactCommand command);
        Task<IResult> DeleteUserBusinessContact(DeleteUserContactCommand command);
    }

    public class BusinessContactRepository : IBusinessContactRepository
    {
        private readonly IUserContactMongoDataContext _dataContext;
        private readonly ILogger<BusinessContactRepository> _logger;

        public BusinessContactRepository(ILogger<BusinessContactRepository> logger,
                                         IUserContactMongoDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IResult<IEnumerable<BusinessContact>>> GetUserContactsPaginated(
            GetUserContactsPaginatedQuery query)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(GetUserContactsPaginated));
            IResult<IEnumerable<BusinessContact>> output;
            try
            {
                var queryableCollection = _dataContext.BusinessContacts.AsQueryable();

                _logger.LogTrace("Creating database query");
                var databaseQuery =  queryableCollection
                    .Where(x => x.Id == query.UserId)
                    .SelectMany(x => x.Contacts)
                    .Skip(query.Offset)
                    .Take(query.ChunkSize)
                    .Select(x => new BusinessContact
                    {
                        Id = x.ContactId,
                        Name = x.Name,
                        EmailAddress = x.EmailAddress,
                        Address = x.Address
                    });

                _logger.LogTrace("Executing database query");
                var list = await databaseQuery.ToListAsync();

                output = Result<IEnumerable<BusinessContact>>.SuccessResult(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactRepository), nameof(GetUserContactsPaginated));
                output = Result<IEnumerable<BusinessContact>>.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(GetUserContactsPaginated));
            return output;
        }

        public async Task<IResult> CreateUserContact(CreateUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(CreateUserContact));
            IResult output;
            try
            {
                _logger.LogTrace("Converting command into data entity.");
                var entity = new BusinessContactDataEntity
                {
                    ContactId = command.ContactId,
                    Name = command.Name,
                    EmailAddress = command.EmailAddress,
                    Address = command.Address
                };

                _logger.LogTrace("Calling {filterName} to get filter.", nameof(GetUserByIdFilter));
                var filter = GetUserByIdFilter(command.UserId);

                _logger.LogTrace("Creating update definition.");
                var update = Builders<UserBusinessContactDataEntity>
                    .Update.Push(user => user.Contacts, entity);

                _logger.LogTrace("Executing update command");
                await _dataContext.BusinessContacts.FindOneAndUpdateAsync(filter, update);
                _logger.LogInformation("Added contact {contactId} to user {userId}", command.ContactId, command.UserId);

                output = Result.SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactRepository), nameof(CreateUserContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(CreateUserContact));
            return output;
        }

        public async Task<IResult> UpdateUserBusinessContact(UpdateUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(UpdateUserBusinessContact));
            IResult output;

            //using var session = await _dataContext.StartSessionAsync();
            try
            {
                var filter = GetUserContactPositionFilterByIdsFilter(command.UserId, command.ContactId);

                var entity = new BusinessContactDataEntity
                {
                    ContactId = command.ContactId,
                    Name = command.Name,
                    EmailAddress = command.EmailAddress,
                    Address = command.Address
                };
                var update = Builders<UserBusinessContactDataEntity>.Update.Set("Contacts.$", entity);

                //session.StartTransaction();
                var updateResult = await _dataContext.BusinessContacts.UpdateOneAsync(filter, update);

                if (updateResult.IsAcknowledged && updateResult.MatchedCount > 0)
                    output = Result.SuccessResult();
                //await session.CommitTransactionAsync();
                else
                    output = Result.FailureResult("User business contact does not exist.");
                //await session.AbortTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactRepository), nameof(UpdateUserBusinessContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(UpdateUserBusinessContact));
            return output;
        }

        public async Task<IResult> DeleteUserBusinessContact(DeleteUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(DeleteUserBusinessContact));
            IResult output;
            //var session = await _dataContext.StartSessionAsync();
            try
            {
                var userFilter = GetUserByIdFilter(command.UserId);
                var contactFilter = GetContactByIdFilter(command.ContactId);
                var update =
                    Builders<UserBusinessContactDataEntity>.Update.PullFilter(user => user.Contacts, contactFilter);

                //session.StartTransaction();
                var deleteResult = await _dataContext.BusinessContacts.UpdateOneAsync(userFilter, update);
                if (deleteResult.IsAcknowledged && deleteResult.ModifiedCount > 0)
                    output = Result.SuccessResult();
                //await session.CommitTransactionAsync();
                else
                    output = Result.FailureResult("User business contact does not exist.");
                //await session.AbortTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactRepository), nameof(DeleteUserBusinessContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(DeleteUserBusinessContact));
            return output;
        }

        private FilterDefinition<UserBusinessContactDataEntity> GetUserContactPositionFilterByIdsFilter(
            string userId, string contactId)
        {
            return GetUserByIdFilter(userId) & GetUserWithContactByIdFilter(contactId);
        }

        private FilterDefinition<UserBusinessContactDataEntity> GetUserByIdFilter(string userId)
        {
            return Builders<UserBusinessContactDataEntity>.Filter.Eq(user => user.Id, userId);
        }

        private FilterDefinition<UserBusinessContactDataEntity> GetUserWithContactByIdFilter(string contactId)
        {
            return Builders<UserBusinessContactDataEntity>
                    .Filter.ElemMatch(user => user.Contacts, GetContactByIdFilter(contactId))
                ;
        }

        private FilterDefinition<BusinessContactDataEntity> GetContactByIdFilter(string contactId)
        {
            return Builders<BusinessContactDataEntity>
                .Filter.Eq(contact => contact.ContactId, contactId);
        }
    }
}