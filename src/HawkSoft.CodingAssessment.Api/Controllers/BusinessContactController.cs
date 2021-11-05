using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HawkSoft.CodingAssessment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessContactController : ControllerBase
    {
        private readonly ILogger<BusinessContactController> _logger;

        public BusinessContactController(ILogger<BusinessContactController> logger)
        {
            _logger = logger;
        }
    }
}
