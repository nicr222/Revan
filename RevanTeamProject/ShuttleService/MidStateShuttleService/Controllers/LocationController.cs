using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class LocationController : Controller
    {
        private readonly string connectionString;
        private readonly ILogger<LocationController> _logger;


        public LocationController(IConfiguration configuration, ILogger<LocationController> logger)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        // GET: LocationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }
            else
            {
                

                TempData["SuccessMessage"] = "The location has been successfully created!";
                
            }
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [Location] (Name, Address, City, State, ZipCode, Abbreviation) VALUES (@Name, @Address, @City, @State, @ZipCode, @Abbreviation)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = location.Name,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Address",
                        Value = location.Address,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@City",
                        Value = location.City,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@State",
                        Value = location.State,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 2
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@ZipCode",
                        Value = location.ZipCode,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 10
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Abbreviation",
                        Value = location.Abbreviation,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 5
                    };
                    command.Parameters.Add(parameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            return RedirectToAction("Index", "Home"); // Assuming "Home" is the controller where you want to redirect
        }


        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
