using Microsoft.AspNetCore.Mvc;

namespace Fynex.Service.WhatsApp.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("API Fynex operativa");
    }
}
