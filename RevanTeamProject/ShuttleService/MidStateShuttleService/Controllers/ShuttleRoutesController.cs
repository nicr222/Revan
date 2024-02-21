using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Controllers
{
    public class ShuttleRoutesController : Controller
    {
        private readonly string connectionString;
        private readonly ILogger<LocationController> _logger;


        public ShuttleRoutesController(IConfiguration configuration, ILogger<LocationController> logger)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        // GET: RouteController
        public ActionResult Index()
        {
            return View();
        }


        // GET: RouteController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Routes routes)
        {
            if(!ModelState.IsValid)
            {
                return View(routes);
            }
            else
            {
                TempData["SuccessMessage"] = "The route has been successfully created!";
            }

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO [Route] (PickUpLocationID, DropOffLocationID, PickUpTime, DropOffTime, AdditionalDetails) VALUES (@PickUpLocationID, @DropOffLocationID, @PickUpTime, @DropOffTime, @AdditionalDetails)";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@PickUpLocationID",
                        Value = routes.PickUpLocationID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DropOffLocationID",
                        Value = routes.DropOffLocationID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@PickUpTime",
                        Value = routes.PickUpTime,
                        SqlDbType = SqlDbType.DateTime
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@DropOffTime",
                        Value = routes.DropOffTime,
                        SqlDbType = SqlDbType.DateTime
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@AdditionalDetails",
                        Value = routes.AdditionalDetails,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 255
                    };
                    command.Parameters.Add(parameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: RouteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RouteController/Edit/5
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

        // GET: RouteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RouteController/Delete/5
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
