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
                /*using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Open connection once for all operations

                    if (model.TripType == "RoundTrip" && model.SpecialRequest == false)
                    {
                        var commandText = @"INSERT INTO [dbo].[Registration] 
                        (FirstName, LastName, Phone, Email, TripType, AgreeToTerms, SelectedRouteDetail, ReturnSelectedRouteDetail, SpecialRequest, FirstDayExpectingToRide) 
                        OUTPUT INSERTED.RegistrationID
                        VALUES 
                        (@FirstName, @LastName, @Phone, @Email, @TripType, @AgreeToTerms, @SelectedRouteDetail, @ReturnSelectedRouteDetail, @SpecialRequest, @FirstDayExpectingToRide)";


                        // Initialize the command with the command text and connection
                        var command = new SqlCommand(commandText, connection);

                        // Add the common parameters that are always included
                        //command.Parameters.AddWithValue("@RouteID", model.RouteID.HasValue ? (object)model.RouteID.Value : DBNull.Value);
                        //command.Parameters.AddWithValue("@UserID", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@TripType", model.TripType);
                        command.Parameters.AddWithValue("@AgreeToTerms", model.AgreeTerms ?? false);
                        command.Parameters.AddWithValue("@SelectedRouteDetail", (object)model.SelectedRouteDetail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReturnSelectedRouteDetail", (object)model.ReturnSelectedRouteDetail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);
                        command.Parameters.AddWithValue("@FirstDayExpectingToRide", model.FirstDayExpectingToRide.HasValue ? (object)model.FirstDayExpectingToRide.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value);

                        // Execute the command and get the new RegistrationID
                        var registrationId = ExecuteSqlCommand(command);

                        if (registrationId > 0)
                        {
                            // Insert the days of the week selected by the user
                            if (model.SelectedDaysOfWeek != null && model.SelectedDaysOfWeek.Any())
                            {
                                InsertSelectedDaysOfWeek(connection, registrationId, model.SelectedDaysOfWeek);
                            }

                            TempData["RegistrationSuccess"] = true;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }

                    // Fetch location names
                    string pickUpLocationName = GetLocationNameById(model.PickUpLocationID);
                    string dropOffLocationName = GetLocationNameById(model.DropOffLocationID);

                    // Check if either location name is 'Other'
                    bool isPickUpLocationOther = string.Equals(pickUpLocationName, "Other", StringComparison.OrdinalIgnoreCase);
                    bool isDropOffLocationOther = string.Equals(dropOffLocationName, "Other", StringComparison.OrdinalIgnoreCase);

                    if (model.TripType == "RoundTrip" && model.SpecialRequest != false || (isPickUpLocationOther || isDropOffLocationOther))
                    {
                        var commandText = @"INSERT INTO [dbo].[Registration] 
                            (FirstName, LastName, Phone, Email, TripType, SpecialRequest, SelectedRouteDetail, MustArriveTime, CanLeaveTime,
                            SpecialPickUpLocation, SpecialDropOffLocation, AgreeToTerms, NeedTransportation, PickUpLocationID, DropOffLocationID) 
                            OUTPUT INSERTED.RegistrationID
                            VALUES 
                            (@FirstName, @LastName, @Phone, @Email, @TripType,  @SpecialRequest, @SelectedRouteDetail, @MustArriveTime, @CanLeaveTime,
                            @SpecialPickUpLocation, @SpecialDropOffLocation, @AgreeToTerms, @NeedTransportation, @PickUpLocationID, @DropOffLocationID)";

                        // Initialize the command with the command text and connection
                        var command = new SqlCommand(commandText, connection);

                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@TripType", model.TripType);
                        command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);
                        command.Parameters.AddWithValue("@SelectedRouteDetail", (object)model.SelectedRouteDetail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PickUpLocationID", model.PickUpLocationID.HasValue ? (object)model.PickUpLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@DropOffLocationID", model.DropOffLocationID.HasValue ? (object)model.DropOffLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@MustArriveTime", model.MustArriveTime.HasValue ? (object)model.MustArriveTime.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CanLeaveTime", model.CanLeaveTime.HasValue ? (object)model.CanLeaveTime.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@SpecialPickUpLocation", string.IsNullOrEmpty(model.SpecialPickUpLocation) ? (object)DBNull.Value : model.SpecialPickUpLocation);
                        command.Parameters.AddWithValue("@SpecialDropOffLocation", string.IsNullOrEmpty(model.SpecialDropOffLocation) ? (object)DBNull.Value : model.SpecialDropOffLocation);
                        command.Parameters.AddWithValue("@AgreeToTerms", model.AgreeTerms ?? false);
                        command.Parameters.AddWithValue("@NeedTransportation", model.NeedTransportation ?? string.Empty);



                        // Execute the command and get the new RegistrationID
                        var registrationId = ExecuteSqlCommand(command);

                        if (registrationId > 0)
                        {
                            TempData["RegistrationSuccess"] = true;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }

                    if (model.TripType == "OneWay" && model.SpecialRequest == false)
                    {
                        var commandText = @"INSERT INTO [dbo].[Registration] 
                        (FirstName, LastName, Phone, Email, TripType, AgreeToTerms, SelectedRouteDetail, SpecialRequest, FirstDayExpectingToRide) 
                        OUTPUT INSERTED.RegistrationID
                        VALUES 
                        (@FirstName, @LastName, @Phone, @Email, @TripType, @AgreeToTerms, @SelectedRouteDetail, @SpecialRequest, @FirstDayExpectingToRide)";


                        // Initialize the command with the command text and connection
                        var command = new SqlCommand(commandText, connection);

                        // Add the common parameters that are always included
                        //command.Parameters.AddWithValue("@RouteID", model.RouteID.HasValue ? (object)model.RouteID.Value : DBNull.Value);
                        //command.Parameters.AddWithValue("@UserID", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@TripType", model.TripType);
                        command.Parameters.AddWithValue("@AgreeToTerms", model.AgreeTerms ?? false);
                        command.Parameters.AddWithValue("@SelectedRouteDetail", (object)model.SelectedRouteDetail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);
                        command.Parameters.AddWithValue("@FirstDayExpectingToRide", model.FirstDayExpectingToRide.HasValue ? (object)model.FirstDayExpectingToRide.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value);

                        // Execute the command and get the new RegistrationID
                        var registrationId = ExecuteSqlCommand(command);

                        if (registrationId > 0)
                        {
                            // Insert the days of the week selected by the user
                            if (model.SelectedDaysOfWeek != null && model.SelectedDaysOfWeek.Any())
                            {
                                InsertSelectedDaysOfWeek(connection, registrationId, model.SelectedDaysOfWeek);
                            }

                            TempData["RegistrationSuccess"] = true;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }

                    if (model.TripType == "OneWay" && model.SpecialRequest != false || (isPickUpLocationOther || isDropOffLocationOther))
                    {
                        var commandText = @"INSERT INTO [dbo].[Registration] 
                            (FirstName, LastName, Phone, Email, TripType, SpecialRequest, SelectedRouteDetail, MustArriveTime, 
                            SpecialPickUpLocation, SpecialDropOffLocation, AgreeToTerms, NeedTransportation, PickUpLocationID, DropOffLocationID) 
                            OUTPUT INSERTED.RegistrationID
                            VALUES 
                            (@FirstName, @LastName, @Phone, @Email, @TripType,  @SpecialRequest, @SelectedRouteDetail, @MustArriveTime, 
                            @SpecialPickUpLocation, @SpecialDropOffLocation, @AgreeToTerms, @NeedTransportation,  @PickUpLocationID, @DropOffLocationID)";

                        // Initialize the command with the command text and connection
                        var command = new SqlCommand(commandText, connection);

                        // Add the common parameters that are always included
                        //command.Parameters.AddWithValue("@RouteID", model.RouteID.HasValue ? (object)model.RouteID.Value : DBNull.Value);
                        //command.Parameters.AddWithValue("@UserID", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@TripType", model.TripType);
                        command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);
                        command.Parameters.AddWithValue("@SelectedRouteDetail", (object)model.SelectedRouteDetail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MustArriveTime", model.MustArriveTime.HasValue ? (object)model.MustArriveTime.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@SpecialPickUpLocation", string.IsNullOrEmpty(model.SpecialPickUpLocation) ? (object)DBNull.Value : model.SpecialPickUpLocation);
                        command.Parameters.AddWithValue("@SpecialDropOffLocation", string.IsNullOrEmpty(model.SpecialDropOffLocation) ? (object)DBNull.Value : model.SpecialDropOffLocation);
                        command.Parameters.AddWithValue("@PickUpLocationID", model.PickUpLocationID.HasValue ? (object)model.PickUpLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@DropOffLocationID", model.DropOffLocationID.HasValue ? (object)model.DropOffLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@AgreeToTerms", model.AgreeTerms ?? false);
                        command.Parameters.AddWithValue("@NeedTransportation", model.NeedTransportation ?? string.Empty);



                        // Execute the command and get the new RegistrationID
                        var registrationId = ExecuteSqlCommand(command);

                        if (registrationId > 0)
                        {
                            TempData["RegistrationSuccess"] = true;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }

                    if (model.TripType == "Friday" && model.SpecialRequest != false)
                    {
                        var commandText = @"INSERT INTO [dbo].[Registration] 
                            (FirstName, LastName, Phone, Email, TripType, SpecialRequest, FridayTripType, MustArriveTime, CanLeaveTime,
                            AgreeToTerms, WhichFriday, PickUpLocationID, DropOffLocationID) 
                            OUTPUT INSERTED.RegistrationID
                            VALUES 
                            (@FirstName, @LastName, @Phone, @Email, @TripType,  @SpecialRequest, @FridayTripType, @MustArriveTime, @CanLeaveTime,
                             @AgreeToTerms, @WhichFriday,  @PickUpLocationID, @DropOffLocationID)";

                        // Initialize the command with the command text and connection
                        var command = new SqlCommand(commandText, connection);

                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Phone", model.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@TripType", model.TripType);
                        command.Parameters.AddWithValue("@SpecialRequest", model.SpecialRequest ?? false);
                        command.Parameters.AddWithValue("@MustArriveTime", model.FridayMustArriveTime.HasValue ? (object)model.FridayMustArriveTime.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CanLeaveTime", model.FridayCanLeaveTime.HasValue ? (object)model.FridayCanLeaveTime.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@FridayTripType", model.FridayTripType ?? string.Empty);
                        command.Parameters.AddWithValue("@PickUpLocationID", model.FridayPickUpLocationID.HasValue ? (object)model.FridayPickUpLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@DropOffLocationID", model.FridayDropOffLocationID.HasValue ? (object)model.FridayDropOffLocationID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@AgreeToTerms", model.FridayAgreeTerms ?? false);
                        command.Parameters.AddWithValue("@WhichFriday", model.WhichFriday ?? string.Empty);

                        // Execute the command and get the new RegistrationID
                        var registrationId = ExecuteSqlCommand(command);

                        if (registrationId > 0)
                        {
                            TempData["RegistrationSuccess"] = true;
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was an error saving the registration, please try again.");
                        }
                    }

                }*/

                rs.AddEntity(model);
            }

            //model.LocationNames = GetLocationNames();
            return View("Index", model);
        }

        private int ExecuteSqlCommand(SqlCommand command)
        {
            try
            {
                var result = command.ExecuteScalar(); // Assuming your table's INSERT operation has been modified to return the new ID
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError("Database insertion error: ", ex);
                ModelState.AddModelError("", "There was a database error, please try again.");
            }

            return 0; // Return 0 to indicate failure
        }

        private void InsertSelectedDaysOfWeek(SqlConnection connection, int registrationId, List<string> selectedDaysOfWeek)
        {
            foreach (var day in selectedDaysOfWeek)
            {
                var commandText = @"INSERT INTO [dbo].[RegistrationDays] (RegistrationID, DayOfWeek) VALUES (@RegistrationID, @DayOfWeek)";
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@RegistrationID", registrationId);
                    command.Parameters.AddWithValue("@DayOfWeek", day);
                    command.ExecuteNonQuery();
                }
            }
        }

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
                        RouteID = r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.PickUpTime} ({r.AdditionalDetails}), Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.DropOffTime}" });
                else
                    formattedRoutesList.Add(new {
                        RouteID = r.RouteID,
                        Detail = $"Leave {ls.getLocationNameById(r.PickUpLocationID)} at {r.PickUpTime}, Arrive at {ls.getLocationNameById(r.DropOffLocationID)} at {r.DropOffTime}"
                    });
            }

            return Json(formattedRoutesList);
        }
    }
}

