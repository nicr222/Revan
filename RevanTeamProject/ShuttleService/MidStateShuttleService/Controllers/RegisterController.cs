using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidStateShuttleService.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MidStateShuttleService.Controllers
{
    public class RegisterController : Controller
    {
        private readonly string connectionString;

        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger, IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
            var model = new RegisterModel();
            model.LocationNames = GetLocationNames();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            if (!ModelState.IsValid)
            {
                model.LocationNames = GetLocationNames();
            }
            return View(model);

        }


        //The method which will get the location names from the database
        private IEnumerable<SelectListItem> GetLocationNames()
        {
            var locations = new List<SelectListItem>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT LocationID, Name FROM Location", connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locations.Add(new SelectListItem
                            {
                                Value = reader["LocationID"].ToString(),
                                Text = reader["Name"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError("Database connection error: ", ex);
                    // Handle exception
                }
            }
            return locations;
        }

        //retrieves route options based on selected pick-up and drop-off locations from a database and returns them as JSON.
        [HttpGet]
        public ActionResult GetRoutes(int pickUpLocationId, int dropOffLocationId)
        {
            var routesList = new List<object>(); // List to hold the route options

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var query = @"
                SELECT 
                    r.RouteID, 
                    r.PickUpTime, 
                    r.DropOffTime, 
                    r.AdditionalDetails, 
                    pl.Name AS PickUpLocationName, 
                    dl.Name AS DropOffLocationName 
                FROM 
                    Routes r
                    INNER JOIN Location pl ON r.PickUpLocationID = pl.LocationID
                    INNER JOIN Location dl ON r.DropOffLocationID = dl.LocationID
                WHERE 
                    r.PickUpLocationID = @PickUpLocationId AND 
                    r.DropOffLocationID = @DropOffLocationId";   // SQL query to select route details based on pick-up and drop-off location 

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PickUpLocationId", pickUpLocationId); // Set the parameter for the pick-up location ID.
                        command.Parameters.AddWithValue("@DropOffLocationId", dropOffLocationId);

                        using (var reader = command.ExecuteReader()) // Execute the command and store the results in a reader.
                        {
                            while (reader.Read())// Iterate through the results.
                            {
                                var routeID = reader.GetInt32(reader.GetOrdinal("RouteID"));
                                var pickUpTime = reader.GetTimeSpan(reader.GetOrdinal("PickUpTime")).ToString(@"hh\:mm");
                                var dropOffTime = reader.GetTimeSpan(reader.GetOrdinal("DropOffTime")).ToString(@"hh\:mm");
                                var additionalDetails = reader.IsDBNull(reader.GetOrdinal("AdditionalDetails")) ? null : reader.GetString(reader.GetOrdinal("AdditionalDetails"));
                                var pickUpLocationName = reader.GetString(reader.GetOrdinal("PickUpLocationName"));
                                var dropOffLocationName = reader.GetString(reader.GetOrdinal("DropOffLocationName"));

                                // Format the route details into a string.
                                // Conditionally construct the routeDetail string
                                var routeDetail = additionalDetails != null
                                    ? $"Leave {pickUpLocationName} at {pickUpTime} ({additionalDetails}), Arrive at {dropOffLocationName} at {dropOffTime}"
                                    : $"Leave {pickUpLocationName} at {pickUpTime}, Arrive at {dropOffLocationName} at {dropOffTime}";

                                routesList.Add(new { RouteID = routeID, Detail = routeDetail }); // Add the route details to the list.
                            }
                        }
                    }
                }
                catch (SqlException ex) // Catch any SQL exceptions.
                {
                    _logger.LogError("Database connection error: ", ex);
                    // Handle exception appropriately
                    return Json(new { error = "Error loading routes. Please try again." });
                }
            }

            return Json(routesList);
        }

    }
}

