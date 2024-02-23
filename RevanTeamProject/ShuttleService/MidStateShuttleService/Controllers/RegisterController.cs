using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidStateShuttleService.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

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
    }
}

