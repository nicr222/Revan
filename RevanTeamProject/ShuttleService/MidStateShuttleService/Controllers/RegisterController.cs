using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;
using System.Diagnostics;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<RegisterController> _logger;

        // Inject ApplicationDbContext into the controller constructor
        public RegisterController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        public IActionResult Index()
        {
            LocationServices ls = new LocationServices(_context);

            var model = new RegisterModel();
            model.LocationNames = ls.GetLocationNames();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult Register()
        {
            LocationServices ls = new LocationServices(_context);

            var model = new RegisterModel();
            model.LocationNames = ls.GetLocationNames();
            return View("Index", model);
        }

        //Completed the backend logic for a registration form submission
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            LocationServices ls = new LocationServices(_context);
            RegisterServices rs = new RegisterServices(_context);

            // Repopulate LocationNames for the model in case of return to View due to invalid model state or any error.
            model.LocationNames = ls.GetLocationNames();

            if (ModelState.IsValid)
            {
                if (rs.AddEntity(model))
                {
                    // Increment the registration count in the session
                    int registrationCount = HttpContext.Session.GetInt32("RegistrationCount") ?? 0;
                    registrationCount++;

                    HttpContext.Session.SetString("RegistrationSuccess", "true"); // Using session to set registration success.
                    HttpContext.Session.SetInt32("RegistrationCount", registrationCount); 

                    TempData["RegistrationSuccess"] = true;

                    Debug.WriteLine(TempData["RegistrationSuccess"]);

                    return RedirectToAction("Index");
                } else
                {
                    ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                }
            }

            //model.LocationNames = GetLocationNames();
            return View("Index", model);
        }

        public ActionResult RegisterConfirmation(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View("Index", model);
        }

        //private int ExecuteSqlCommand(SqlCommand command)
        //{
        //    try
        //    {
        //        var result = command.ExecuteScalar(); // Assuming your table's INSERT operation has been modified to return the new ID
        //        if (result != null)
        //        {
        //            return Convert.ToInt32(result);
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        _logger.LogError("Database insertion error: ", ex);
        //        ModelState.AddModelError("", "There was a database error, please try again.");
        //    }

        //    return 0; // Return 0 to indicate failure
        //}

        //private void InsertSelectedDaysOfWeek(SqlConnection connection, int registrationId, List<string> selectedDaysOfWeek)
        //{
        //    foreach (var day in selectedDaysOfWeek)
        //    {
        //        var commandText = @"INSERT INTO [dbo].[RegistrationDays] (RegistrationID, DayOfWeek) VALUES (@RegistrationID, @DayOfWeek)";
        //        using (var command = new SqlCommand(commandText, connection))
        //        {
        //            command.Parameters.AddWithValue("@RegistrationID", registrationId);
        //            command.Parameters.AddWithValue("@DayOfWeek", day);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //retrieves route options based on selected pick-up and drop-off locations from a database and returns them as JSON.
        [HttpGet]
        public ActionResult GetRoutes(int pickUpLocationId, int dropOffLocationId)
        {
            RouteServices rs = new RouteServices(_context);
            var routesList = rs.GetRoutesByLocations(pickUpLocationId, dropOffLocationId); // List to hold the route options
            LocationServices ls = new LocationServices(_context);

            var formattedRoutesList = new List<object>();
            foreach( var r in routesList)
            {
                if (r.AdditionalDetails != null)
                    formattedRoutesList.Add(new {
                        r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.PickUpTime} ({r.AdditionalDetails}), Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.DropOffTime}" });
                else
                    formattedRoutesList.Add(new {
                        r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.PickUpTime}, Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.DropOffTime}"
                    });
            }

            return Json(formattedRoutesList);
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the student to be edited from the database
            var student = _context.RegisterModels.Find(id);

            if (student == null)
            {
                return NotFound(); // Or handle the case where the student is not found
            }

            return View(student);
        }

        // POST: RegisterController/Edit/5
        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RegisterModel student)
        {
            if (id != student.RegistrationId)
            {
                return BadRequest(); // Or handle the case where IDs do not match
            }

            if (!ModelState.IsValid)
            {
                return View(student); // Return the view with validation errors
            }

            try
            {
                // Update the student in the database
                _context.Update(student);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "The student has been successfully updated!";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context); // Log SQL exception
                _logger.LogError(ex, "An error occurred while updating student.");
                ModelState.AddModelError("", "An unexpected error occurred, please try again.");
                return View(student); // Return the view with an error message
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var student = _context.RegisterModels.Find(id);



                _context.RegisterModels.Remove(student);
                _context.SaveChanges();

                return RedirectToAction("Index", "Dashboard"); // Redirect to Index after successful deletion
            }
            catch (Exception ex)
            {
                // Log the SQL exception and any other exceptions
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while deleting student.");

                // Optionally add a model error for displaying an error message to the user
                ModelState.AddModelError("", "An unexpected error occurred while deleting the student, please try again.");

                // Return the view with an error message
                return View();
            }
        }


        // POST: RegisterController/Delete/5
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

