using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;
using System.Diagnostics;
using MidStateShuttleService.Service;
using MidStateShuttleService.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

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

        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult Register()
        {
            LocationServices ls = new LocationServices(_context);

            var model = new RegisterModel();
            model.LocationNames = ls.GetLocationNames();
            return View("Index", model);
        }

        //Completed the backend logic for a registration form submission
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            LocationServices ls = new LocationServices(_context);
            RegisterServices rs = new RegisterServices(_context);
            EmailServices es = new EmailServices();

            // Repopulate LocationNames for the model in case of return to View due to invalid model state or any error.
            model.LocationNames = ls.GetLocationNames();

            if (ModelState.IsValid)
            {
                model.IsActive = true; // Set IsActive to true
                model.DeviceIpAddress = model.DeviceIpAddress ?? "Unknown"; // Default to "Unknown" if IP is null
                model.InsertDateTime = DateTime.Now;

                // Use LocationServices to fetch the location names based on the foreign keys
                var pickupLocationName = ls.getLocationNameById(model.PickUpLocationID.Value);
                var dropoffLocationName = ls.getLocationNameById(model.DropOffLocationID.Value);

                if (rs.AddEntity(model))
                {
                    // Increment the registration count in the session
                    int registrationCount = HttpContext.Session.GetInt32("RegistrationCount") ?? 0;
                    registrationCount++;

                    HttpContext.Session.SetString("RegistrationSuccess", "true"); // Using session to set registration success.
                    HttpContext.Session.SetInt32("RegistrationCount", registrationCount);

                    TempData["RegistrationSuccess"] = true;

                    string emailBody = GenerateRegistrationEmailBody(model);
                    es.SendEmail(model.Email, "MSTC Shuttle Service Request", emailBody, isHtml: true);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There was an error saving the request, please try again.");
                }
            }

            //model.LocationNames = GetLocationNames();
            return View("Index", model);
        }


        [AllowAnonymous]
        public ActionResult RegisterConfirmation(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View("Index", model);
        }


        //retrieves route options based on selected pick-up and drop-off locations from a database and returns them as JSON.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetRoutes(int pickUpLocationId, int dropOffLocationId)
        {
            RouteServices rs = new RouteServices(_context);
            // This call will now also check the IsActive property of each route
            var routesList = rs.GetRoutesByLocations(pickUpLocationId, dropOffLocationId)
                               .Where(route => route.IsActive).ToList(); // Filte
            LocationServices ls = new LocationServices(_context);

            var formattedRoutesList = new List<object>();
            foreach( var r in routesList)
            {
                if (r.AdditionalDetails != null)
                    formattedRoutesList.Add(new {
                        r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.ToStringPickUpTime()} ({r.AdditionalDetails}), Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.ToStringDropOffTime()}" });
                else
                    formattedRoutesList.Add(new {
                        r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.ToStringPickUpTime()}, Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.ToStringDropOffTime()}"
                    });
            }

            return Json(formattedRoutesList);
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            LocationServices ls = new LocationServices(_context);
            RouteServices rs = new RouteServices(_context);

            // Retrieve the student to be edited from the database
            var student = _context.RegisterModels.Find(id);

            if (student == null)
            {
                return NotFound(); // Or handle the case where the student is not found
            }

            // Retrieve the days of the week selected for the student
            var selectedDaysOfWeek = _context.RegisterModels
                                              .Where(s => s.RegistrationId == id)
                                              .Select(s => s.SelectedDaysOfWeek)
                                              .FirstOrDefault();

            // Pass the selected days of the week to the view
            ViewBag.SelectedDaysOfWeek = selectedDaysOfWeek;

            ViewBag.RouteList = rs.GetAllEntities();

            ViewBag.SelectedPickupRoute = student.SelectedRouteDetail;
            ViewBag.SelectedReturnRoute = student.ReturnSelectedRouteDetail;

            // Return the location names for each route
            foreach(Routes route in ViewBag.RouteList)
            {
                route.PickUpLocation = ls.GetEntityById(route.PickUpLocationID);
                route.DropOffLocation = ls.GetEntityById(route.DropOffLocationID);
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

            // Make sure the return route is null if the student selected one way
            if (student.TripType == "OneWay")
            {
                student.ReturnSelectedRouteDetail = null;
            }
            
            if (!ModelState.IsValid)
            {
                return View(student); // Return the view with validation errors
            }

            try
            {
                // Update the student in the database
                student.IsActive = true; // Set IsActive to true
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

                if (student != null)
                {
                    student.IsActive = !student.IsActive; // Toggle IsActive from true to false or false to true
                    _context.SaveChanges();
                }
                else
                {
                    // Handle the case where the student with the specified id is not found
                    ModelState.AddModelError("", "Student not found.");
                    return View();
                }

                return RedirectToAction("Index", "Dashboard"); // Redirect after toggling IsActive
            }
            catch (Exception ex)
            {
                // Log the exception
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while toggling IsActive of the student.");

                // Optionally add a model error for displaying an error message to the user
                ModelState.AddModelError("", "An unexpected error occurred while toggling IsActive of the student, please try again.");

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

        /// <summary>
        /// Email content to generate for a registration (requested ride) confirmation email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerateRegistrationEmailBody(RegisterModel model)
        {
            try
            {
                // Check if the pick-up and drop-off locations are valid
                if (model.PickUpLocationID == null || model.DropOffLocationID == null)
                {
                    return "Invalid pick-up or drop-off location";
                }
                else
                {
                    var actionResult = GetRoutes(model.PickUpLocationID.Value, model.DropOffLocationID.Value);
                    string initialRoute = "Unknown";

                    // Extract the JSON content from ActionResult
                    if (actionResult is JsonResult jsonResult)
                    {
                        string jsonString = JsonSerializer.Serialize(jsonResult.Value);

                        // Parse the JSON string
                        using JsonDocument doc = JsonDocument.Parse(jsonString);

                        // Assuming the first route in the list is required
                        initialRoute = doc.RootElement[0].GetProperty("Detail").GetString();
                    }

                    return $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; }}
                            .email-container {{ max-width: 600px; margin: auto; padding: 20px; }}
                            .header {{ text-align: center; }}
                            .content {{ margin-top: 20px; }}
                            .footer {{ margin-top: 30px; text-align: center; font-size: 12px; color: gray; }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <h2>MSTC Shuttle Service Request Confirmation</h2>
                            </div>
                            <div class='content'>
                                <p><strong>Student ID:</strong> {model.StudentId}</p>
                                <p><strong>First Name:</strong> {model.FirstName}</p>
                                <p><strong>Last Name:</strong> {model.LastName}</p>
                                <p><strong>Email:</strong> {model.Email}</p>
                                <p><strong>Phone Number:</strong> {model.PhoneNumber}</p>
                                <p><strong>Initial Route:</strong> {initialRoute}</p>
                                <p><strong>Trip Type:</strong> {model.TripType}</p>
                                <p><strong>Days of the Week Needed:</strong> {string.Join(", ", model.SelectedDaysOfWeek)}</p>
                                <p><strong>First Day Expecting to Ride:</strong> {model.FirstDayExpectingToRide?.ToString("MM-dd-yyyy")}</p>
                                <p><strong>Contact Preference:</strong> {model.ContactPreference}</p>
                            </div>
                            <div class='footer'>
                                <p>If you have any questions, please call or text: <strong>715-581-9284</strong></p>
                            </div>
                        </div>
                    </body>
                    </html>";
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while generating request email body.");
                return "An error occurred while generating request email body.";
            }

            
        }
    }
}

