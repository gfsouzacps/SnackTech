using Microsoft.AspNetCore.Mvc;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Get()
            => Ok("Live!");
    }
}
