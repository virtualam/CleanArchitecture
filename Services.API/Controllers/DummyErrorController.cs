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
            _logger.LogInformation("Getting requested resource");

            _logger.LogDebug("Entering action...");

            var obj = new { P = 1, S = "H", R = true, Z = DateTime.Now };
            _logger.LogWarning("Sample warning {@obj}!", obj);
            _logger.LogError(new Exception("Invalid Id"), "Sample error!");
            try
            {
                _CreateException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "_CreateException() threw error.");
            }
            _logger.LogCritical(new Exception("Invalid program"), "Sample critical!");
            _logger.LogTrace("Sample trace!");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        private void _CreateException()
        {
            throw new Exception("Something has gone haywire!");
        }
    }
}