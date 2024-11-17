using Microsoft.AspNetCore.Mvc;

namespace RedAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RedController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostList([FromBody] List<string> list) => Ok(list);

        [HttpGet]
        public IActionResult GetList() => Ok(new List<string>() { "red" });
    }
}
