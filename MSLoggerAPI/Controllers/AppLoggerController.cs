using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MSLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppLoggerController : ControllerBase
    {
        public AppLoggerController() { }

        public IActionResult GetAll()
        {
            return null;
        }
    }
}
