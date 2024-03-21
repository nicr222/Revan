using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class ShuttlesController : Controller
    {

        private readonly string connectionString;
        private readonly ILogger<DriverController> _logger;

        public ShuttlesController(IConfiguration configuration, ILogger<DriverController> logger)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        // GET: ShuttlesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShuttlesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShuttlesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShuttlesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bus bus)
        {
            if (!ModelState.IsValid)
            {
                return View(bus);
            }
            else
            {
                TempData["SuccessMessage"] = "The bus has been successfully created!";
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [Bus] (BusNo, PassengerCapacity, DriverID) VALUES (@BusNo, @PassengerCapacity, @DriverID)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@BusNo",
                        Value = bus.BusNo,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@PassengerCapacity",
                        Value = bus.PassengerCapacity,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DriverID",
                        Value = bus.DriverId,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index", "Dashboard");

        }

        // GET: ShuttlesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShuttlesController/Edit/5
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

        // GET: ShuttlesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShuttlesController/Delete/5
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
