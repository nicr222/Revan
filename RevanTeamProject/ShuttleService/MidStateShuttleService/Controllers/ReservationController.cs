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
            // replace list and reservation with DB queried info
            var viewModel = new Tuple<List<string>, Reservation>
            (
                new List<string>
                {
                    "Wisconsin Rapids",
                    "Stevens Point",
                    "Wausua",
                    "Adams"
                },

                new Reservation(1, 1, 1, "rapids", "rapids", new DateOnly(), new TimeOnly())
            );

            return View(viewModel);
        }
    }
}

