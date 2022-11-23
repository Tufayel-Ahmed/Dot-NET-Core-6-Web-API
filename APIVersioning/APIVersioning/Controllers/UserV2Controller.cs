using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIVersioning.Controllers
{
    //Route for query string parameter versioning
    [Route("api/user/[action]")]
    //Route for URI versioning
    //[Route("api/v{version: apiVersion}/user/[action]")]
    [ApiController]
    [ApiVersion("2")]
    public class UserV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok("User v2 controller");
        }
    }
}
