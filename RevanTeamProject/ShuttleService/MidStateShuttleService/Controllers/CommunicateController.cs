using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Controllers
{
    public class CommunicateController : Controller
    {
        private readonly string connectionString;

        private readonly ILogger<CommunicateController> _logger;

        public CommunicateController(ILogger<CommunicateController> logger, IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CommuncateModel();
            model.LocationNames = GetLocationNames();
            return View(model);
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult Index(CommuncateModel c)
        {

            c.LocationNames = GetLocationNames();

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve passed in list of students from the database.

                    // Send the message to each person registered to the route.

                    // Save message to database -- commented out until Driver table is set up
                    /*using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string sendMessageQuery = "INSERT INTO [dbo].[Message] (Message, BusRiderId, DriverId) VALUES (@Message, @BusRiderId, @DriverId); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdMessage = new SqlCommand(sendMessageQuery, connection);

                        cmdMessage.Parameters.AddWithValue("@Message", c.message);
                        cmdMessage.Parameters.AddWithValue("@BusRiderId", );
                        cmdMessage.Parameters.AddWithValue("@DriverId", );
                        cmdMessage.ExecuteNonQuery();
                    }*/

                    return RedirectToAction("MessageSent");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error Sending Message");

                    return View("Error");
                }
            }

            
            return View(c);
        }

        public IActionResult MessageSent()
        {
            return View();
        }

        /// <summary>
        /// Displays the view for the student's communication form
        /// </summary>
        /// <returns> The Student Communicate View </returns>
        public IActionResult StudentCommunicate()
        {
            return View();
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult StudentCommunicate(Message c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string sendMessageQuery = "INSERT INTO [dbo].[DispatchMessage] (Message, Name, ResponseRequired, ContactInfo) VALUES (@Message, @Name, @ResponseRequired, @ContactInfo); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdMessage = new SqlCommand(sendMessageQuery, connection);

                        cmdMessage.Parameters.AddWithValue("@Message", c.message);
                        cmdMessage.Parameters.AddWithValue("@Name", c.name);
                        cmdMessage.Parameters.AddWithValue("@ResponseRequired", c.responseRequired);
                        if (c.responseRequired == false)
                        {
                            cmdMessage.Parameters.AddWithValue("@ContactInfo", "null");
                        }
                        else
                        {
                            cmdMessage.Parameters.AddWithValue("@ContactInfo", c.contactInfo);
                        }
                        cmdMessage.ExecuteNonQuery();
                    }

                    return RedirectToAction("MessageSent");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error Sending Message");

                    return View("Error");
                }
            }

            return View(c);
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
