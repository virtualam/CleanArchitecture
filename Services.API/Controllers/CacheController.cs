using Microsoft.AspNetCore.Http;
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
    [Produces("application/json")]
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

        /// <summary>
        /// Gets a stored cache value
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/[controller]/{key}
        /// </remarks>
        /// <param name="key"></param>
        /// <returns>A stored value if key is found</returns>
        /// <response code="200">If the key is found</response>
        /// <response code="404">If the key is not found</response>
        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            _logger.LogDebug("Requested cache key '{@key}'", key);

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
