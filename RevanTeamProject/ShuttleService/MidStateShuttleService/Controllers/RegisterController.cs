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
            var model = new RegisterModel();
            model.LocationNames = GetLocationNames();
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
            var model = new RegisterModel();
            model.LocationNames = GetLocationNames();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            // Repopulate LocationNames for the model in case of return to View due to invalid model state or any error.
            model.LocationNames = GetLocationNames();

            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var commandText = @"INSERT INTO [dbo].[Registration] 
                                (RouteID, UserID, FirstName, LastName, Phone, Email, TripType, AgreeToTerms, SelectedRouteDetail, ReturnSelectedRouteDetail, SpecialRequest) 
                                VALUES 
                                (@RouteID, @UserID, @FirstName, @LastName, @Phone, @Email, @TripType, @AgreeToTerms, @SelectedRouteDetail, @ReturnSelectedRouteDetail, @SpecialRequest)";

                    // Initialize the command with the command text and connection
                    var command = new SqlCommand(commandText, connection);

                    // Add the common parameters that are always included
                    command.Parameters.AddWithValue("@RouteID", model.RouteID.HasValue ? (object)model.RouteID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@UserID", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@TripType", model.TripType);
                    command.Parameters.AddWithValue("@AgreeToTerms", model.AgreeTerms ?? false);
                    //command.Parameters.AddWithValue("@PickUpLocationID", model.PickUpLocationID.HasValue ? (object)model.PickUpLocationID.Value : DBNull.Value);
                    //command.Parameters.AddWithValue("@DropOffLocationID", model.DropOffLocationID.HasValue ? (object)model.DropOffLocationID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SelectedRouteDetail", (object)model.SelectedRouteDetail ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReturnSelectedRouteDetail", (object)model.ReturnSelectedRouteDetail ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);

                    // Check if SpecialRequest is No and TripType is not Friday, then ignore the special request related fields
                    if (!(model.SpecialRequest ?? false) && model.TripType != "Friday")
                    {
                        // You can set default values or handle the database defaults for the fields you're ignoring
                        // For example, setting default values for nullable fields that are being ignored
                        // command.Parameters.AddWithValue("@SomeField", DBNull.Value);
                    }

                    try
                    {
                        connection.Open(); 
                        var result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            //model.LocationNames = GetLocationNames();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        _logger.LogError("Database insertion error: ", ex);
                        ModelState.AddModelError("", "There was a database error, please try again.");
                    }
                }
            }

            //model.LocationNames = GetLocationNames();
            return View("Index", model);
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

        //retrieves route options based on selected pick-up and drop-off locations from a database and returns them as JSON.
        [HttpGet]
        public ActionResult ReturnGetRoutes(int returnpickUpLocationId, int returndropOffLocationId)
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
                        command.Parameters.AddWithValue("@PickUpLocationId", returnpickUpLocationId); // Set the parameter for the pick-up location ID.
                        command.Parameters.AddWithValue("@DropOffLocationId", returndropOffLocationId);

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

