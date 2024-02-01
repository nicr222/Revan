using Microsoft.AspNetCore.Mvc;

namespace MidStateShuttleService.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckIn(int id)
        {
            return View(id);
        }
    }
}
