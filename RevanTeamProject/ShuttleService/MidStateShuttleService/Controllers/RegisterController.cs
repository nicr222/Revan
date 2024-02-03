using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidStateShuttleService.Models;
using System.Diagnostics;

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
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid || model.SpecialRequest == null)
            {
                // Here you would implement the logic to store the data in the database.
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    // Insert User
                    command.CommandText = "INSERT INTO Users (StudentId, FirstName, LastName, PhoneNumber) VALUES (@StudentId, @FirstName, @LastName, @PhoneNumber); SELECT SCOPE_IDENTITY();";
                    command.Parameters.AddWithValue("@StudentId", model.StudentId);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    var userId = (int)(decimal)command.ExecuteScalar();

                    // Insert Trip
                    command.CommandText = "INSERT INTO Trips (UserId, TripType, PickUpLocation, DropOffLocation, Date, Time) VALUES (@UserId, @TripType, @PickUpLocation, @DropOffLocation, @Date, @Time);";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@TripType", model.TripType);
                    command.Parameters.AddWithValue("@PickUpLocation", model.PickUpLocation);
                    command.Parameters.AddWithValue("@DropOffLocation", model.DropOffLocation);
                    command.Parameters.AddWithValue("@Date", model.Date);
                    command.Parameters.AddWithValue("@Time", model.Time);

                    // Assign "No" to SpecialRequest if it's null
                    var specialRequest = string.IsNullOrWhiteSpace(model.SpecialRequest) ? "No" : model.SpecialRequest;
                    command.Parameters.AddWithValue("@SpecialRequest", specialRequest);

                    // If you decide to include SpecialRequest in some cases, add it conditionally here
                    if (!string.IsNullOrWhiteSpace(model.SpecialRequest))
                    {
                        // Optionally handle special request here
                    }
                    command.ExecuteNonQuery();
                }

                // Redirect to confirmation page (assuming you have a confirmation action and view ready)
                return RedirectToAction("RegisterConfirmation", new { id = model.StudentId }); // Adjust according to your confirmation page setup
            }

            // If model state is not valid, return back to the form with the model to show validation errors
            return View(model);
        }

        public ActionResult RegisterConfirmation(int id)
        {
            RegisterModel model = new RegisterModel(); // Placeholder for the actual model

            // Fetching the user and trip details from the database
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // Assuming you're identifying the user and trip uniquely with StudentId
                // This query needs adjustment based on the actual database schema and requirements
                command.CommandText = @"
                                        SELECT u.StudentId, u.FirstName, u.LastName, u.PhoneNumber, t.TripType, t.PickUpLocation, t.DropOffLocation, t.Date, t.Time, t.SpecialRequest
                                        FROM Users u
                                        INNER JOIN Trips t ON u.UserId = t.UserId
                                        WHERE u.StudentId = @StudentId;";
                command.Parameters.AddWithValue("@StudentId", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Assuming at least one record is returned
                    {
                        model.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        model.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        model.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        model.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                        model.TripType = reader.GetString(reader.GetOrdinal("TripType"));
                        model.PickUpLocation = reader.GetString(reader.GetOrdinal("PickUpLocation"));
                        model.DropOffLocation = reader.GetString(reader.GetOrdinal("DropOffLocation"));
                        model.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                        // Correctly handle the TimeSpan to DateTime conversion
                        TimeSpan timeSpan = (TimeSpan)reader.GetValue(reader.GetOrdinal("Time"));
                        model.Time = DateTime.Today.Add(timeSpan); // This sets the Time part on today's date, adjust as necessary

                        // Assign "No" to SpecialRequest if it's null
                        var specialRequest = string.IsNullOrWhiteSpace(model.SpecialRequest) ? "No" : model.SpecialRequest;
                        model.SpecialRequest = specialRequest;
                    }
                }
            }

            return View(model); // Pass the populated model to the view
        }


    }
}
