using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Repositories;
using HawkSoft.CodingAssessment.Models;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Services
{
    public interface IBusinessContactService
    {
        Task<IResult<IEnumerable<BusinessContact>>> GetUserBusinessContactsPaginated(
            string userId, int offset, int chunkSize);
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

        private IResult ValidateUserPaginationValues(string userId, int offset, int chunkSize)
        {
            return Result.SuccessResult();
        }
    }
}