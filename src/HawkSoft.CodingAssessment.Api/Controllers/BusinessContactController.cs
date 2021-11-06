using System.Threading.Tasks;
using HawkSoft.CodingAssessment.Api.Services;
using HawkSoft.CodingAssessment.Data.Repositories;
using HawkSoft.CodingAssessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessContactController : ControllerBase
    {
        private readonly ILogger<BusinessContactController> _logger;
        private readonly IResultNotaryService _resultNotaryService;
        private readonly IBusinessContactService _contactService;

        public BusinessContactController(ILogger<BusinessContactController> logger,
                                         IResultNotaryService resultNotaryService,
                                         IBusinessContactService contactService)
        {
            _logger = logger;
            _resultNotaryService = resultNotaryService;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId, int offset = 0, int chunkSize = 10)
        {
            var result = await _contactService.GetUserBusinessContactsPaginated(userId, offset, chunkSize);
            return _resultNotaryService.NotarizeResult(result);
        }
    }
}