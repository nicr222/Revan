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

            // The temp data which is used to display the modal after sending a form
            HttpContext.Session.SetString("CheckInSuccess", "true");
            TempData["CheckInSuccess"] = true;

            return RedirectToAction("CheckIn");
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
            try
            {
                CheckInServices cs = new CheckInServices(_context);
                CheckIn model = cs.GetEntityById(id);

                if (model == null)
                    return FailedCheckIn("Check In could not be found");

                model.IsActive = !model.IsActive; // Toggle IsActive value
                cs.UpdateEntity(model); // Update the entity in the database

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                // Log the exception
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while toggling IsActive of the check in.");

                // Optionally add a model error for displaying an error message to the user
                ModelState.AddModelError("", "An unexpected error occurred while toggling IsActive of the check in, please try again.");

                // Return the view with an error message or handle the error as required
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult FailedCheckIn(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View("FailedCheckIn");
        }
    }
}
