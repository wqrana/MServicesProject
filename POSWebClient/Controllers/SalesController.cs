using Microsoft.AspNetCore.Mvc;

namespace POSWebClient.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
