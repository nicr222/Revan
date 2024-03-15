using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class DriverController : Controller
    {

        private readonly string connectionString;
        private readonly ILogger<DriverController> _logger;

        public DriverController(IConfiguration configuration, ILogger<DriverController> logger)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        // GET: DriverController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return View(driver);
            }
            else
            {


                TempData["SuccessMessage"] = "The driver has been successfully created!";

            }
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [Driver] (Name, PhoneNumb, Email, IsActive) VALUES (@Name, @PhoneNumb, @Email, 1)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = driver.Name,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@PhoneNumb",
                        Value = driver.PhoneNumber,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 20
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = driver.Email,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            return RedirectToAction("Index", "Dashboard"); // Assuming "Home" is the controller where you want to redirect
        }
        

        // GET: DriverController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DriverController/Edit/5
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

        // GET: DriverController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DriverController/Delete/5
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
