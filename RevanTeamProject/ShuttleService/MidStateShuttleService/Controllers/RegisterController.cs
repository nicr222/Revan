using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidStateShuttleService.Models;
using MidStateShuttleService.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using MidStateShuttleService.Service;
using System.Data;

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
                    TempData["RegistrationSuccess"] = true;
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
    }
}

