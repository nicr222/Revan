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

        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public CommunicateController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }
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
    }
}
