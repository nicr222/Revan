using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class RoutesController : Controller
    {
        private readonly string connectionString;
        private readonly ILogger<LocationController> _logger;


        public RoutesController(IConfiguration configuration, ILogger<LocationController> logger)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }


        // GET: RoutesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoutesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoutesController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: RoutesController/Edit/
[HttpPost]
        public ActionResult Create(Routes route)
        {
            

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                string sql = "INSERT INTO [Routes] (PickUpLocationID, DropOffLocationID, PickUpTime, DropOffTime, AdditionalDetails) VALUES (@PickUpLocationID, @DropOffLocationID, @PickUpTime, @DropOffTime, @AdditionalDetails)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    // Adding parameters
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@PickUpLocationID",
                        Value = route.PickUpLocationID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DropOffLocationID",
                        Value = route.DropOffLocationID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@PickUpTime",
                        Value = route.PickUpTime,
                        SqlDbType = SqlDbType.Time
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DropOffTime",
                        Value = route.DropOffTime,
                        SqlDbType = SqlDbType.Time
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@AdditionalDetails",
                        Value = (object)route.AdditionalDetails ?? DBNull.Value,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 255 // You can adjust the size based on your requirements
                    };
                    command.Parameters.Add(parameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index", "Home"); // Assuming "Home" is the controller where you want to redirect
        }


        // GET: RoutesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoutesController/Delete/5
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
