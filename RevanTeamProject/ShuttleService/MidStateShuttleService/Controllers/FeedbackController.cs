using Microsoft.AspNetCore.Mvc;

namespace MidStateShuttleService.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
