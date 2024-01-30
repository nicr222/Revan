using Microsoft.AspNetCore.Mvc;

namespace MidStateShuttleService.Controllers
{
    public class CommunicateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
