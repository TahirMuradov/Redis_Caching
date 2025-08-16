using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_MemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public ExampleController(IMemoryCache cache)
        {
            _cache = cache;
        }
        [HttpGet("get/{key}")]
        public IActionResult Get(string key)
        {
            if (_cache.TryGetValue<string>(key, out string? value))
            {
                return Ok(value);
            }
            return NotFound("Key not found in cache.");
        }
        [HttpPost("set/{key}")]
        public IActionResult Set(string key, [FromBody] string value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
            _cache.Set(key, value, cacheEntryOptions);
            return Ok("Value set in cache.");
        }
        [HttpDelete("remove/{key}")]
        public IActionResult Remove(string key)
        {
            if (_cache.TryGetValue(key, out _))
            {
                _cache.Remove(key);
                return Ok("Key removed from cache.");
            }
            return NotFound("Key not found in cache.");
        }
    }
}
