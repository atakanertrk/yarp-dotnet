using Microsoft.AspNetCore.Mvc;

namespace BlueAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlueController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostList([FromBody] List<string> list) => Ok(list);

        [HttpGet]
        public IActionResult GetList() => Ok(new List<string>() { "blue" });
    }
}
