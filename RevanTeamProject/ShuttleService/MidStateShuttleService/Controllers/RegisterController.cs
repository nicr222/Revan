using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidStateShuttleService.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

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
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert into Rider Table
                        //Currently set userId as nullable in the model, so it's not included in the insert query
                        string riderInsertQuery = "INSERT INTO [dbo].[Rider] (FirstName, LastName, Type, Phone, Email) VALUES (@FirstName, @LastName, @Type, @Phone, @Email); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdRider = new SqlCommand(riderInsertQuery, connection);
                        //cmdRider.Parameters.AddWithValue("@UserId", model.UserId);
                        cmdRider.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmdRider.Parameters.AddWithValue("@LastName", model.LastName);
                        cmdRider.Parameters.AddWithValue("@Type", model.TripType); // Assuming 'Type' is the trip type for simplicity
                        cmdRider.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        cmdRider.Parameters.AddWithValue("@Email", model.Email);
                        int riderId = Convert.ToInt32(cmdRider.ExecuteScalar());

                        // Insert into Route Table
                        string routeInsertQuery = "INSERT INTO [dbo].[Route] (PickLocationID, DropOffLocationID, PickUpTime, DropOffTime) VALUES (@PickLocationID, @DropOffLocationID, @PickUpTime, @DropOffTime); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdRoute = new SqlCommand(routeInsertQuery, connection);
                        cmdRoute.Parameters.AddWithValue("@PickLocationID", model.PickLocationID);
                        cmdRoute.Parameters.AddWithValue("@DropOffLocationID", model.DropOffLocationID);

                        cmdRoute.Parameters.AddWithValue("@PickUpTime", model.PickUpTime);
                        cmdRoute.Parameters.AddWithValue("@DropOffTime", model.DropOffTime); // Adjust as necessary
                        int routeId = Convert.ToInt32(cmdRoute.ExecuteScalar());

                        // Insert into Reservation Table
                        string reservationInsertQuery = "INSERT INTO [dbo].[Reservation] (RiderID, Date, SpecialRequest) VALUES (@RiderID, @Date, @SpecialRequest);";
                        SqlCommand cmdReservation = new SqlCommand(reservationInsertQuery, connection);
                        cmdReservation.Parameters.AddWithValue("@RiderID", riderId);
                        cmdReservation.Parameters.AddWithValue("@Date", model.Date);
                        cmdReservation.Parameters.AddWithValue("@SpecialRequest", (bool)model.SpecialRequest ? 1 : 0); // Convert bool to bit
                        cmdReservation.ExecuteNonQuery();
                    }

                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error registering shuttle reservation");
                    // Handle the error (e.g., show an error message)
                    return View("Error");
                }
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }

     
        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    // Remove the ModelState entry for SpecialRequest to ignore its validation
        //    //ModelState.Remove("SpecialRequest");

        //    if (ModelState.IsValid || model.SpecialRequest == null)
        //    {
        //        // Here you would implement the logic to store the data in the database.
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            var command = connection.CreateCommand();

        //            // Insert User
        //            command.CommandText = "INSERT INTO Users (StudentId, FirstName, LastName, PhoneNumber) VALUES (@StudentId, @FirstName, @LastName, @PhoneNumber); SELECT SCOPE_IDENTITY();";
        //            command.Parameters.AddWithValue("@StudentId", model.UserId);
        //            command.Parameters.AddWithValue("@FirstName", model.FirstName);
        //            command.Parameters.AddWithValue("@LastName", model.LastName);
        //            command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
        //            //var userId = (int)(decimal)command.ExecuteScalar();
        //            var userId = Convert.ToInt32(command.ExecuteScalar()); // This is fine as userId is expected to be within INT range

        //            // Insert Trip
        //            command.CommandText = "INSERT INTO Trips (UserId, TripType, PickUpLocation, DropOffLocation, Date, Time) VALUES (@UserId, @TripType, @PickUpLocation, @DropOffLocation, @Date, @Time);";
        //            command.Parameters.AddWithValue("@UserId", userId);
        //            command.Parameters.AddWithValue("@TripType", model.TripType);
        //            command.Parameters.AddWithValue("@PickUpLocation", model.PickLocationID);
        //            command.Parameters.AddWithValue("@DropOffLocation", model.DropOffLocationID);
        //            command.Parameters.AddWithValue("@Date", model.Date);
        //            command.Parameters.AddWithValue("@Time", model.Time);

        //            //// Assign "No" to SpecialRequest if it's null
        //            //var specialRequest = string.IsNullOrWhiteSpace(model.SpecialRequest) ? "No" : model.SpecialRequest;
        //            //command.Parameters.AddWithValue("@SpecialRequest", specialRequest);

        //            //// If you decide to include SpecialRequest in some cases, add it conditionally here
        //            //if (!string.IsNullOrWhiteSpace(model.SpecialRequest))
        //            {
        //                // Optionally handle special request here
        //            }
        //            command.ExecuteNonQuery();
        //        }

        //        _logger.LogInformation("Registration successful for Student ID: {StudentId}", model.UserId);

        //        // Redirect to confirmation page (assuming you have a confirmation action and view ready)
        //        return RedirectToAction("RegisterConfirmation", new { id = model.UserId }); // Adjust according to confirmation page setup
        //    }

        //    else
        //    {
        //        _logger.LogWarning("Registration failed validation for Student ID: {UserId}.", model.UserId);

        //        // Return the form with validation errors
        //        return View(model);
        //    }
        //}

    }
}
