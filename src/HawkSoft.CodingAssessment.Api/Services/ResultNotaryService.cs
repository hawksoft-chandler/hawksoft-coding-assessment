using HawkSoft.CodingAssessment.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HawkSoft.CodingAssessment.Api.Services
{
    public interface IResultNotaryService
    {
        IActionResult NotarizeResult(IResult result);
        IActionResult NotarizeResult<T>(IResult<T> result);
    }

    public class ResultNotaryService : IResultNotaryService
    {
        public IActionResult NotarizeResult<T>(IResult<T> result)
        {
            IActionResult output;
            if (result.Success)
            {
                output = new OkObjectResult(result.Value);
            }
            else
            {
                if (result.IsException)
                    output = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                else
                    output = new BadRequestResult();
            }

            return output;
        }

        public IActionResult NotarizeResult(IResult result)
        {
            IActionResult output;
            if (result.Success)
            {
                output = new OkResult();
            }
            else
            {
                if (result.IsException)
                    output = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                else
                    output = new BadRequestResult();
            }

            return output;
        }
    }
}