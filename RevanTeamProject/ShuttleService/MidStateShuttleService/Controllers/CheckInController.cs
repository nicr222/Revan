using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public CheckInController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        // GET: CheckInController/Create
        public ActionResult CheckIn()
        {
            return View();
        }

        // POST: CheckInController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn(CheckIn checkIn)
        {
            //get bus id buy bus number
            BusServices bs = new BusServices(_context);
            var result = bs.FindBusByNumber(checkIn.BusNumber);

            if (result == null)
                return View(FailedCheckIn("Could Not Find Shuttle"));

            checkIn.Bus = result;
            checkIn.BusId = checkIn.Bus.BusId;


            //Need to find current route

            //date
            checkIn.Date = DateTime.Now;

            _context.CheckIns.Add(checkIn);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public ActionResult FailedCheckIn(string errorMessage)
        {

            return View();
        }
    }
}
