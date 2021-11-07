using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Api.Models.Requests;
using HawkSoft.CodingAssessment.Api.Services;
using HawkSoft.CodingAssessment.Models.Commands;
using HawkSoft.CodingAssessment.Models.Queries;
using HawkSoft.CodingAssessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessContactController : ControllerBase
    {
        private readonly IBusinessContactService _contactService;
        private readonly ILogger<BusinessContactController> _logger;
        private readonly IResultNotaryService _resultNotaryService;

        public BusinessContactController(ILogger<BusinessContactController> logger,
                                         IResultNotaryService resultNotaryService,
                                         IBusinessContactService contactService)
        {
            _logger = logger;
            _resultNotaryService = resultNotaryService;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserContactsPaginatedRequest request)
        {
            var query = new GetUserContactsPaginatedQuery
            {
                UserId = request.UserId,
                Offset = request.Offset,
                ChunkSize = request.ChunkSize
            };
            var result =
                await _contactService.GetUserBusinessContactsPaginated(query);
            return _resultNotaryService.NotarizeResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBusinessContactRequest request)
        {
            var command = new CreateUserContactCommand
            {
                UserId = request.UserId,
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                Address = request.Address
            };

            var result = await _contactService.CreateUserBusinessContact(command);
            return _resultNotaryService.NotarizeResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserContactRequest request)
        {
            var command = new UpdateUserContactCommand()
            {
                UserId = request.UserId,
                ContactId = request.ContactId,
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                Address = request.Address
            };
            var result = await _contactService.UpdateUserBusinessContact(command);
            return _resultNotaryService.NotarizeResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserContactRequest request)
        {
            var command = new DeleteUserContactCommand
            {
                UserId = request.UserId,
                ContactId = request.ContactId
            };

            var result = await _contactService.DeleteUserBusinessContact(command);
            return _resultNotaryService.NotarizeResult(result);
        }
    }
}