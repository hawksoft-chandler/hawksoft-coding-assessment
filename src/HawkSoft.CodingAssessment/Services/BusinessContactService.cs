using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Common;
using HawkSoft.CodingAssessment.Data.Repositories;
using HawkSoft.CodingAssessment.Models;
using HawkSoft.CodingAssessment.Models.Commands;
using HawkSoft.CodingAssessment.Models.Queries;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Services
{
    public interface IBusinessContactService
    {
        Task<IResult<IEnumerable<BusinessContact>>> GetUserBusinessContactsPaginated(
            GetUserContactsPaginatedQuery query);

        Task<IResult> CreateUserBusinessContact(CreateUserContactCommand command);
        Task<IResult> UpdateUserBusinessContact(UpdateUserContactCommand command);
        Task<IResult> DeleteUserBusinessContact(DeleteUserContactCommand command);
    }

    public class BusinessContactService : IBusinessContactService
    {
        private readonly IBusinessContactRepository _contactRepository;
        private readonly ILogger<BusinessContactService> _logger;

        public BusinessContactService(ILogger<BusinessContactService> logger,
                                      IBusinessContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        public async Task<IResult<IEnumerable<BusinessContact>>> GetUserBusinessContactsPaginated(
            GetUserContactsPaginatedQuery query)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(GetUserBusinessContactsPaginated));
            IResult<IEnumerable<BusinessContact>> output;
            try
            {
                _logger.LogTrace("Calling function {validationFunction}",
                    nameof(ValidateGetUserContactsPaginatedQuery));
                var validationResult = ValidateGetUserContactsPaginatedQuery(query);
                if (validationResult.Success)
                {
                    _logger.LogTrace("Validation succeeded.");
                    _logger.LogTrace("Calling function {repositoryName}.{functionName}()",
                        nameof(IBusinessContactRepository),
                        nameof(IBusinessContactRepository.GetUserContactsPaginated));
                    output = await _contactRepository.GetUserContactsPaginated(query);
                }
                else
                {
                    _logger.LogWarning("Failed to validate {parameter}. Failure Messages: {failureMessages}",
                        nameof(GetUserContactsPaginatedQuery), validationResult.FailureMessages);
                    _logger.LogDebug("{invalidParameterName} value: {invalidParameter}",
                        nameof(GetUserContactsPaginatedQuery),
                        JsonSerializer.Serialize(query));
                    output = Result<IEnumerable<BusinessContact>>.FailureResult(validationResult.FailureMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactService), nameof(GetUserBusinessContactsPaginated));
                output = Result<IEnumerable<BusinessContact>>.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(GetUserBusinessContactsPaginated));
            return output;
        }

        public async Task<IResult> CreateUserBusinessContact(CreateUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(CreateUserBusinessContact));
            IResult output;
            try
            {
                _logger.LogTrace("Calling function {validationFunction}", nameof(ValidateCreateUserContactCommand));
                var validationResult = ValidateCreateUserContactCommand(command);
                if (validationResult.Success)
                {
                    _logger.LogTrace("Validation succeeded.");
                    _logger.LogTrace("Calling function {repositoryName}.{functionName}()",
                        nameof(IBusinessContactRepository),
                        nameof(IBusinessContactRepository.CreateUserContact));
                    output = await _contactRepository.CreateUserContact(command);
                }
                else
                {
                    _logger.LogWarning("Failed to validate {parameter}. Failure Messages: {failureMessages}",
                        nameof(CreateUserContactCommand), validationResult.FailureMessages);
                    _logger.LogDebug("{invalidParameterName} value: {invalidParameter}",
                        nameof(CreateUserContactCommand),
                        JsonSerializer.Serialize(command));
                    output = Result.FailureResult(validationResult.FailureMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactService), nameof(CreateUserBusinessContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(CreateUserBusinessContact));
            return output;
        }

        public async Task<IResult> UpdateUserBusinessContact(UpdateUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(UpdateUserBusinessContact));
            IResult output;
            try
            {
                _logger.LogTrace("Calling function {functionName}", nameof(ValidateUpdateUserContactCommand));
                var validationResult = ValidateUpdateUserContactCommand(command);
                if (validationResult.Success)
                {
                    _logger.LogTrace("Validation succeeded.");
                    _logger.LogTrace("Calling function {repositoryName}.{functionName}()",
                        nameof(IBusinessContactRepository),
                        nameof(IBusinessContactRepository.UpdateUserBusinessContact));
                    output = await _contactRepository.UpdateUserBusinessContact(command);
                }
                else
                {
                    _logger.LogWarning("Failed to validate {parameter}. Failure Messages: {failureMessages}",
                        nameof(UpdateUserContactCommand), validationResult.FailureMessages);
                    _logger.LogDebug("{invalidParameterName} value: {invalidParameter}",
                        nameof(UpdateUserContactCommand),
                        JsonSerializer.Serialize(command));
                    output = Result.FailureResult(validationResult.FailureMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactService), nameof(UpdateUserBusinessContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(UpdateUserBusinessContact));
            return output;
        }

        public async Task<IResult> DeleteUserBusinessContact(DeleteUserContactCommand command)
        {
            _logger.LogTrace("Entering function {functionName}", nameof(DeleteUserBusinessContact));
            IResult output;
            try
            {
                _logger.LogTrace("Calling function {functionName}", nameof(ValidateDeleteUserContactCommand));
                var validationResult = ValidateDeleteUserContactCommand(command);
                if (validationResult.Success)
                {
                    _logger.LogTrace("Validation succeeded.");
                    _logger.LogTrace("Calling function {repositoryName}.{functionName}()",
                        nameof(IBusinessContactRepository),
                        nameof(IBusinessContactRepository.DeleteUserBusinessContact));
                    output = await _contactRepository.DeleteUserBusinessContact(command);
                }
                else
                {
                    _logger.LogWarning("Failed to validate {parameter}. Failure Messages: {failureMessages}",
                        nameof(DeleteUserContactCommand), validationResult.FailureMessages);
                    _logger.LogDebug("{invalidParameterName} value: {invalidParameter}",
                        nameof(DeleteUserContactCommand),
                        JsonSerializer.Serialize(command));
                    output = Result.FailureResult(validationResult.FailureMessages);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{serviceName}.{functionName}: Unhandled exception caught.",
                    nameof(BusinessContactService), nameof(DeleteUserBusinessContact));
                output = Result.ExceptionResult(ex);
            }

            _logger.LogTrace("Exiting function {functionName}", nameof(DeleteUserBusinessContact));
            return output;
        }

        private IResult ValidateDeleteUserContactCommand(DeleteUserContactCommand command)
        {
            // Here we would validate the values against business rules
            return Result.SuccessResult();
        }

        private IResult ValidateUpdateUserContactCommand(UpdateUserContactCommand command)
        {
            // Here we would validate the values against business rules
            return Result.SuccessResult();
        }

        private IResult ValidateCreateUserContactCommand(CreateUserContactCommand command)
        {
            // Here we would validate the values against business rules
            return Result.SuccessResult();
        }

        private IResult ValidateGetUserContactsPaginatedQuery(GetUserContactsPaginatedQuery query)
        {
            // Here we would validate the values against business rules
            return Result.SuccessResult();
        }
    }
}