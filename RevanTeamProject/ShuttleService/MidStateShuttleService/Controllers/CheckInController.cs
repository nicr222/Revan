using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CheckInController> _logger;

        // Inject ApplicationDbContext into the controller constructor
        public CheckInController(ApplicationDbContext context, ILogger<CheckInController> logger)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
            _logger = logger;
        }

        // GET: CheckInController/Create
        public ActionResult CheckIn()
        {
            LocationServices ls = new LocationServices(_context);
            ViewBag.Locations = ls.GetAllEntities().Select(x => new SelectListItem { Text = x.Name, Value = x.LocationId.ToString() });

            return View();

        }

        public ActionResult EditCheckIn(int id)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(id);

            LocationServices ls = new LocationServices(_context);
            ViewBag.Locations = ls.GetAllEntities().Select(x => new SelectListItem { Text = x.Name, Value = x.LocationId.ToString() });

            if (model == null)
                return FailedCheckIn("Check In Not Found");

            return View(model);
        }

        // POST: CheckInController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn(CheckIn checkIn)
        {
                   

            //date
            checkIn.Date = DateTime.Now;
            CheckInServices cs = new CheckInServices(_context);
            cs.AddEntity(checkIn);

            // Increment the check-in count in the session
            int checkInCount = HttpContext.Session.GetInt32("CheckInCount") ?? 0;
            checkInCount++;
            HttpContext.Session.SetInt32("CheckInCount", checkInCount);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCheckIn(CheckIn checkIn)
        {
            CheckInServices cs = new CheckInServices(_context);
            if (checkIn == null)
                return FailedCheckIn("Updates to check in could not be applied");

            //not all values comming over from form
            cs.UpdateEntity(checkIn);

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult DeleteCheckIn(int id)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(id);

            if (model == null)
                return FailedCheckIn("Check In Not Found");

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(id);
            if (model == null)
                return FailedCheckIn("Check In Could not be found");

            cs.DeleteEntity(model.CheckInId);

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult FailedCheckIn(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View("FailedCheckIn");
        }
    }
}
