using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly string connectionString;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IConfiguration Configuration { get; }

        // GET: DashboardController
        public ActionResult Index()
        {
            List<Location> locations = GetLocationList();
            List<Routes> routes = GetRoutes();
            List<Driver> drivers = GetDrivers();
            List<Bus> buss = GetBus();



            AllModels allModels = new AllModels();


            allModels.Location = locations;
            allModels.Route = routes;
            allModels.Driver = drivers;
            allModels.Bus = buss;

            return View(allModels);
            
        }



        public List<Location> GetLocationList()
        {
            List<Location> locationList = new List<Location>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Location]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    locationList.Add(new Location
                    {
                        LocationId = Convert.ToInt32(row["LocationID"]),
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        City = row["City"].ToString(),
                        State = row["State"].ToString(),
                        ZipCode = row["ZipCode"].ToString(),
                        Abbreviation = row["Abbreviation"].ToString()
                    });
                }
            }
            return locationList;
        }

        public List<Routes> GetRoutes()
        {
            List<Routes> routeList = new List<Routes>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var location = GetLocationList();

                connection.Open();

                string sql = "SELECT * FROM [Routes]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Routes routes = new Routes();

                        routes.RouteID = Convert.ToInt32(dataReader["RouteID"]);

                        routes.PickUpLocationID = Convert.ToInt32(dataReader["PickUpLocationID"]);
                        routes.PickUpLocation = location.Where(x => x.LocationId == routes.PickUpLocationID).FirstOrDefault();
                        routes.DropOffLocationID = Convert.ToInt32(dataReader["DropOffLocationID"]);
                        routes.DropOffLocation = location.Where(x => x.LocationId == routes.DropOffLocationID).FirstOrDefault();
                        routes.PickUpTime = TimeSpan.Parse(dataReader["PickUpTime"].ToString());
                        routes.DropOffTime = TimeSpan.Parse(dataReader["DropOffTime"].ToString());
                        routes.AdditionalDetails = dataReader["AdditionalDetails"].ToString();
                        routes.IsArchived = Convert.ToBoolean(dataReader["IsArchived"]);

                        routeList.Add(routes);
                    }
                }
                connection.Close();
            }
            
            
            
            return routeList;
        }

        public List<Driver> GetDrivers()
        {
            List<Driver> drivers = new List<Driver>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {

                connection.Open();

                string sql = "SELECT * FROM [Driver]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Driver driver = new Driver();

                        driver.DriverId = Convert.ToInt32(dataReader["DriverID"]);
                        driver.Name = dataReader["Name"].ToString();
                        driver.PhoneNumber = dataReader["PhoneNumb"].ToString();
                        driver.Email = dataReader["Email"].ToString();
                        driver.IsActive = Convert.ToBoolean(dataReader["IsActive"]);

                        drivers.Add(driver);
                    }
                }
            }
            return drivers;
        }

        public List<Bus> GetBus()
        {
            List<Bus> buss = new List<Bus>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {

                connection.Open();

                string sql = "SELECT * FROM [Bus]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Bus bus = new Bus();

                        bus.BusId = Convert.ToInt32(dataReader["BusID"]);
                        bus.BusNo = Convert.ToInt32(dataReader["BusNumber"]);
                        bus.PassengerCapacity = Convert.ToInt32(dataReader["PassengerCapacity"]);
                        bus.DriverId = Convert.ToInt32(dataReader["DriverID"]);


                        buss.Add(bus);
                    }
                }
            }   
            return buss;
        }
        
    }
}
