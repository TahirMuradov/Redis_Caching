using Distributed_Caching.Redis;
using Microsoft.AspNetCore.Mvc;

namespace Distributed_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IRedisHelper _redisHelper;
        public ExampleController(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }
        [HttpGet("get/{key}")]
        public IActionResult Get(string key)
        {
            var value = _redisHelper.GetString(key);
            if (value != null)
            {
                return Ok(value);
            }
            return NotFound("Key not found in cache.");
        }
        [HttpPost("set/{key}")]
        public IActionResult Set(string key, [FromBody] string value)
        {
            if (_redisHelper.SetString(key, value))
            {
                return Ok("Value set in cache.");
            }
            return BadRequest("Failed to set value in cache.");
        }
        [HttpDelete("remove/{key}")]
        public IActionResult Remove(string key)
        {
            if (_redisHelper.RemoveString(key))
            {
                return Ok("Key removed from cache.");
            }
            return NotFound("Key not found in cache.");
        }
    }
}
