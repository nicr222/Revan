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
            //Reservation reservation = DB get reservation by ID
            return View(/* reservation */);
        }
    }
}
