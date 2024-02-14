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
            this.connectionString = configuration.GetConnectionString("Connection");
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
                        string riderInsertQuery = "INSERT INTO [dbo].[Rider] (UserId, FirstName, LastName, Type, Phone, Email) VALUES (@UserId, @FirstName, @LastName, @Type, @Phone, @Email); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmdRider = new SqlCommand(riderInsertQuery, connection);
                        cmdRider.Parameters.AddWithValue("@UserId", model.UserId);
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
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        // Save the data to the database.
        //        // Replace this with your actual database saving logic.
        //        var saveSuccess = SaveReservationToDatabase(model);

        //        if (saveSuccess)
        //        {
        //            // If the save was successful, send the confirmation email.
        //            try
        //            {
        //                await SendConfirmationEmailAsync(model);
        //                ViewBag.Message = "Reservation confirmed. Confirmation email sent.";
        //                return View("Confirmation"); // Redirect to a confirmation page or display a success message.
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log the exception
        //                _logger.LogError(ex, "Error sending confirmation email.");
        //                ModelState.AddModelError("", "An error occurred while sending the confirmation email.");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "An error occurred while saving the reservation.");
        //        }
        //    }

        //    // If we get here, something went wrong; return to the form to display validation errors or other messages.
        //    return View(model); ;
        //}

        //private async Task SendConfirmationEmailAsync(RegisterModel model)
        //{
        //    var smtpSettings = Configuration.GetSection("SmtpSettings");
        //    using (var client = new SmtpClient())
        //    {
        //        client.Host = smtpSettings["Server"];
        //        client.Port = int.Parse(smtpSettings["Port"]);
        //        client.EnableSsl = bool.Parse(smtpSettings["EnableSSL"]);
        //        client.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
        //            Subject = "Shuttle Reservation Confirmation",
        //            Body = CreateEmailBody(model),
        //            IsBodyHtml = true // Set to true if you want to use HTML tags in your email
        //        };

        //        mailMessage.To.Add(new MailAddress(model.Email));

        //        await client.SendMailAsync(mailMessage);
        //    }
        //}

        //private string CreateEmailBody(RegisterModel model)
        //{
        //    // You can use a more sophisticated templating system or StringBuilder for larger templates
        //    string body = $"<h1>Shuttle Reservation Confirmation</h1>" +
        //                  $"<p>Dear {model.FirstName} {model.LastName},</p>" +
        //                  $"<p>Your reservation has been confirmed with the following details:</p>" +
        //                  $"<ul>" +
        //                  $"<li>User ID: {model.UserId}</li>" +
        //                  $"<li>Email: {model.Email}</li>" +
        //                  $"<li>Phone Number: {model.PhoneNumber}</li>" +
        //                  $"<li>Trip Type: {model.TripType}</li>" +
        //                  $"<li>Pick-Up Location ID: {model.PickLocationID}</li>" +
        //                  $"<li>Drop-Off Location ID: {model.DropOffLocationID}</li>" +
        //                  $"<li>Date: {model.Date.ToString("MM/dd/yyyy")}</li>" +
        //                  $"<li>Time: {model.Time.ToString("HH:mm")}</li>" +
        //                  $"<li>Contact Preference: {model.ContactPreference}</li>" +
        //                  $"</ul>" +
        //                  $"<p>Please contact us if you have any questions about your reservation.</p>";

        //    return body;
        //}

        //private bool SaveReservationToDatabase(RegisterModel model)
        //{
        //    bool saveSuccessful = false;

        //    // Use ADO.NET to save to the database.
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        // Open connection.
        //        connection.Open();

        //        // Create a command.
        //        using (var command = connection.CreateCommand())
        //        {
        //            // Set up command text (SQL or stored procedure).
        //            command.CommandText = "INSERT INTO Reservation ..."; // Your SQL INSERT command.

        //            // Add parameters from the model to prevent SQL injection.
        //            command.Parameters.AddWithValue("@UserId", model.UserId);
        //            command.Parameters.AddWithValue("@FirstName", model.FirstName);
        //            command.Parameters.AddWithValue("@LastName", model.LastName);
        //            command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
        //            command.Parameters.AddWithValue("@PickLocationID", model.PickLocationID);
        //            command.Parameters.AddWithValue("@DropOffLocationID", model.DropOffLocationID);
        //            command.Parameters.AddWithValue("@Date", model.Date);
        //            command.Parameters.AddWithValue("@Time", model.Time);
        //            command.Parameters.AddWithValue("@ContactPreference", model.ContactPreference);
        //            // Add other parameters as necessary.

        //            // Execute the command.
        //            int result = command.ExecuteNonQuery();
        //            saveSuccessful = result > 0;
        //        }
        //    }

        //    return saveSuccessful;
        //}


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

        //public ActionResult RegisterConfirmation(long id)
        //{
        //    RegisterModel model = new RegisterModel(); // Placeholder for the actual model

        //    // Fetching the user and trip details from the database
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        var command = connection.CreateCommand();

        //        // Assuming you're identifying the user and trip uniquely with StudentId
        //        // This query needs adjustment based on the actual database schema and requirements
        //        command.CommandText = @"
        //                                SELECT u.StudentId, u.FirstName, u.LastName, u.PhoneNumber, t.TripType, t.PickUpLocation, t.DropOffLocation, t.Date, t.Time, t.SpecialRequest
        //                                FROM Users u
        //                                INNER JOIN Trips t ON u.UserId = t.UserId
        //                                WHERE u.StudentId = @StudentId;";
        //        command.Parameters.AddWithValue("@StudentId", id);

        //        using (var reader = command.ExecuteReader())
        //        {
        //            if (reader.Read()) // Assuming at least one record is returned
        //            {
        //                //model.UserId = reader.GetInt64(reader.GetOrdinal("UserId"));
        //                model.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
        //                model.LastName = reader.GetString(reader.GetOrdinal("LastName"));
        //                model.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
        //                model.TripType = reader.GetString(reader.GetOrdinal("TripType"));
        //                //model.PickLocationID = reader.GetString(reader.GetOrdinal("PickUpLocation"));
        //                //model.DropOffLocationID = reader.GetString(reader.GetOrdinal("DropOffLocation"));
        //                model.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
        //                // Correctly handle the TimeSpan to DateTime conversion
        //                TimeSpan timeSpan = (TimeSpan)reader.GetValue(reader.GetOrdinal("Time"));
        //                model.Time = DateTime.Today.Add(timeSpan); // This sets the Time part on today's date, adjust as necessary

        //                // Assign "No" to SpecialRequest if it's null
        //                //var specialRequest = string.IsNullOrWhiteSpace(model.SpecialRequest) ? "No" : model.SpecialRequest;
        //                //model.SpecialRequest = specialRequest;
        //            }
        //        }
        //    }

        //    return View(model); // Pass the populated model to the view
        //}


    }
}
