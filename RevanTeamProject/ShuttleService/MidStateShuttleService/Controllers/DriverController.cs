using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class DriverController : Controller
    {
        private readonly ILogger<DriverController> _logger;
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public DriverController(ApplicationDbContext context, ILogger<DriverController> logger)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
            _logger = logger; // Assign the injected ILogger to the _logger field
        }

        // GET: DriverController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return View(driver);
            }

            try
            {
                DriverServices ds = new DriverServices(_context);
                ds.AddEntity(driver);

                TempData["SuccessMessage"] = "The driver has been successfully created!";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context); // Log SQL exception
                _logger.LogError(ex, "An error occurred while creating driver.");
                ModelState.AddModelError("", "An unexpected error occurred, please try again.");
                return View(driver);
            }

            
        }



        // GET: DriverController/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the driver to be edited from the database
            var driver = _context.Drivers.Find(id);

            if (driver == null)
            {
                return NotFound(); // Or handle the case where the driver is not found
            }

            return View(driver);
        }

        // POST: DriverController/Edit/5
        // POST: DriverController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest(); // Or handle the case where IDs do not match
            }

            if (!ModelState.IsValid)
            {
                return View(driver); // Return the view with validation errors
            }

            try
            {
                // Update the driver in the database
                _context.Update(driver);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "The driver has been successfully updated!";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context); // Log SQL exception
                _logger.LogError(ex, "An error occurred while updating driver.");
                ModelState.AddModelError("", "An unexpected error occurred, please try again.");
                return View(driver); // Return the view with an error message
            }
        }

        // GET: DriverController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var driver = _context.Drivers.Find(id);

               

                _context.Drivers.Remove(driver);
                _context.SaveChanges();

                return RedirectToAction("Index", "Dashboard"); // Redirect to Index after successful deletion
            }
            catch (Exception ex)
            {
                // Log the SQL exception and any other exceptions
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while deleting driver.");

                // Optionally add a model error for displaying an error message to the user
                ModelState.AddModelError("", "An unexpected error occurred while deleting the driver, please try again.");

                // Return the view with an error message
                return View();
            }
        }


        // POST: DriverController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
