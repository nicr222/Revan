using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;

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
            

            return View();
        }

        [HttpPost]
        public IActionResult CheckIn()
        {
            
            return RedirectToAction("Index", "Home");
        }
    }
}

