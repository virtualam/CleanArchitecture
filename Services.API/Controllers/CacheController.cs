using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<CacheController> _logger;

        public CacheController(IDistributedCache distributedCache, ILogger<CacheController> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            _logger.LogInformation("Requested cache key '{@key}'", key);
            var obj = new { P = 1, S = "H", R = true, Z = DateTime.Now };
            _logger.LogWarning("Sample warning {@obj}!", obj);
            _logger.LogError(new Exception("Invalid Id"), "Sample error!");
            _logger.LogCritical(new Exception("Invalid program"), "Sample critical!");
            _logger.LogTrace("Sample trace!");

            var cachedResponse = await _distributedCache.GetStringAsync(key.ToLower());

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                _logger.LogDebug("Response for cache key '{@key}' is {@cachedResponse}", key, cachedResponse);
                return Ok(cachedResponse);
            }
            else
            {
                _logger.LogDebug("Response for cache key '{@key}' not found", key);
                return (IActionResult)NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheValue([FromBody] NewCacheEntryRequest newCacheEntryRequest)
        {
            try
            {
                await _distributedCache.SetStringAsync(newCacheEntryRequest.Key.ToLower(), newCacheEntryRequest.Value);

                return Created(new Uri($"/api/cache/{newCacheEntryRequest.Key}", UriKind.Relative), newCacheEntryRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, new { Errors = new { Message = ex.Message } });
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteCacheKey([FromRoute] string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, new { Errors = new { Message = ex.Message } });
            }
        }

        public class NewCacheEntryRequest
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
