using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GcpNetCoreAppSerilog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var key = new { name = "Hello World!" };
            _logger.LogDebug("Requested cache key '{@key}'", key);
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
        }
        private void _CreateException()
        {
            throw new Exception("Something has gone haywire!");
        }
    }
}
