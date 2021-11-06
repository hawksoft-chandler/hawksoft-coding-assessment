using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Repositories;
using HawkSoft.CodingAssessment.Models;
using HawkSoft.CodingAssessment.Models.Commands;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Services
{
    public interface IBusinessContactService
    {
        Task<IResult<IEnumerable<BusinessContact>>> GetUserBusinessContactsPaginated(
            string userId, int offset, int chunkSize);

        Task<IResult> CreateUserBusinessContact(CreateUserContactCommand command);
    }

    public class BusinessContactService : IBusinessContactService
    {
        private readonly ILogger<BusinessContactService> _logger;
        private readonly IBusinessContactRepository _contactRepository;

        public BusinessContactService(ILogger<BusinessContactService> logger,
                                      IBusinessContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        public async Task<IResult<IEnumerable<BusinessContact>>> GetUserBusinessContactsPaginated(
            string userId, int offset, int chunkSize)
        {
            IResult<IEnumerable<BusinessContact>> output;
            try
            {
                var validationResult = ValidateUserPaginationValues(userId, offset, chunkSize);
                if (validationResult.Success)
                    output = await _contactRepository.GetUserContactsPaginated(userId, offset, chunkSize);
                else
                    output = Result<IEnumerable<BusinessContact>>.FailureResult(validationResult.FailureMessages);
            }
            catch (Exception ex)
            {
                output = Result<IEnumerable<BusinessContact>>.ExceptionResult(ex);
            }

            return output;
        }

        public async Task<IResult> CreateUserBusinessContact(CreateUserContactCommand command)
        {
            IResult output;
            try
            {
                var validationResult = ValidateCreateUserContactCommand(command);
                if (validationResult.Success)
                {
                    output = await _contactRepository.CreateUserContact(command);
                }
                else
                {
                    output = Result.FailureResult(validationResult.FailureMessages);
                }
            }
            catch(Exception ex)
            {
                output = Result.ExceptionResult(ex);
            }

            return output;
        }

        private IResult ValidateCreateUserContactCommand(CreateUserContactCommand command)
        {
            // Here we would validate the values against business rules
            command.ContactId = Guid.NewGuid().ToString();
            return Result.SuccessResult();
        }
        private IResult ValidateUserPaginationValues(string userId, int offset, int chunkSize)
        {
            // Here we would validate the values against business rules
            return Result.SuccessResult();
        }
    }
}