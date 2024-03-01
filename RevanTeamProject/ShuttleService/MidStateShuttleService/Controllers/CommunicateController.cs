using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult Index(CommuncateModel c)
        {
            if (ModelState.IsValid)
            {
                // Retrieve passed in list of students from the database.

                // Send the message to each person registered to the shutte.
                return RedirectToAction("MessageSent");
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
    }
}
