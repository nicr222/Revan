using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public ActionResult CheckIn()
        {
            LocationServices ls = new LocationServices(_context);
            ViewBag.Locations = ls.GetAllEntities().Select(x => new SelectListItem { Text = x.Name, Value = x.LocationId.ToString() });

            return View();

        }

        // Changing the terminology from check in to registration
        public ActionResult EditCheckIn(int id)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(id);

            LocationServices ls = new LocationServices(_context);
            ViewBag.Locations = ls.GetAllEntities().Select(x => new SelectListItem { Text = x.Name, Value = x.LocationId.ToString() });

            if (model == null)
                return FailedCheckIn("Registration Not Found");

            return View(model);
        }

        // POST: CheckInController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CheckIn(CheckIn checkIn)
        {
                   

            //date
            checkIn.Date = DateTime.Now;
            CheckInServices cs = new CheckInServices(_context);
            checkIn.IsActive = true;
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
                // Changing check in to registration
                return FailedCheckIn("Updates to registration could not be applied");

            //not all values comming over from form
            checkIn.IsActive = true;
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
                    // Changing check in to registration
                    return FailedCheckIn("Registration could not be found");

                model.IsActive = !model.IsActive; // Toggle IsActive value
                cs.UpdateEntity(model); // Update the entity in the database

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                // Log the exception
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                // Changing check in to registration
                _logger.LogError(ex, "An error occurred while toggling IsActive of the registration.");

                // Optionally add a model error for displaying an error message to the user
                // Changing check in to registration
                ModelState.AddModelError("", "An unexpected error occurred while toggling IsActive of the registration, please try again.");

                // Return the view with an error message or handle the error as required
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult FailedCheckIn(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View("FailedCheckIn");
        }
    }
}
