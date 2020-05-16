using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Services.API.Controllers
{
    [ApiController]
    [Route("/dummy-error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DummyErrorController : ControllerBase
    {
        private readonly ILogger<DummyErrorController> _logger;

        public DummyErrorController(ILogger<DummyErrorController> logger)
        {
            _logger = logger;
        }

        public IActionResult Get()
        {
            _logger.LogTrace("Tracing...");

            _logger.LogInformation("Accessing database...");

            var obj = new { FirstName = "John", LastName = "Doe", IsActive = true, DateOfBirth = DateTime.Now };
            _logger.LogDebug("Person info = {@obj}!", obj);

            _logger.LogWarning("Database DTU is nearing it's limits...");
            try
            {
                _CreateException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "_CreateException() threw error.");
            }

            _logger.LogCritical(new Exception("Database access issue"), "Unable to access the cache database");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        private void _CreateException()
        {
            throw new Exception("Something has gone haywire!");
        }
    }
}