using MidStateShuttleService.Models;
using MidStateShuttleService.Models.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;
using Route = MidStateShuttleService.Models.Data.Route;
using Location = MidStateShuttleService.Models.Location;

namespace MidStateShuttleService.Service
{
    public class ListServices : IListService
    {
        public IConfiguration Configuration { get; }

        private readonly string connectionString;

        public ListServices(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public IEnumerable<Location> GetLocationList()
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
                        LocationID = Convert.ToInt32(row["LocationID"]),
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

        public Route GetRouteByID(int id)
        {
            Route route = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Route] WHERE ID = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime pickupDateTime = (DateTime)row["PickUpTime"];
                    TimeOnly pickupTime = new TimeOnly(pickupDateTime.TimeOfDay.Hours, pickupDateTime.TimeOfDay.Minutes);
                    DateTime dropoffDateTime = (DateTime)row["DropOffTime"];
                    TimeOnly dropoffTime = new TimeOnly(pickupDateTime.TimeOfDay.Hours, pickupDateTime.TimeOfDay.Minutes);

                    route = new Route
                    {
                        RouteId = Convert.ToInt32(row["RouteID"]),
                        PickUpLocationId = Convert.ToInt32(row["PickUpLocationID"]),
                        DropOffLocationId = Convert.ToInt32(row["DropOffLocationID"]),
                        PickUpTime = pickupTime,
                        DropOffTime = dropoffTime,
                        AdditionalDetails = row["ZipCode"].ToString(),
                    };
                }
            }
            return route;
        }

        public Route GetCurrentRouteByBusId(int id)
        {
            Route currentRoute = new Route();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                // finds routes linked to the bus ID
                string sql = "SELECT RouteID FROM [BusRoute] WHERE BusID = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                // creates a list of routes
                List<Route> routes = new List<Route>();
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime pickupDateTime = (DateTime)row["PickUpTime"];
                    TimeOnly pickupTime = new TimeOnly(pickupDateTime.TimeOfDay.Hours, pickupDateTime.TimeOfDay.Minutes);
                    DateTime dropoffDateTime = (DateTime)row["DropOffTime"];
                    TimeOnly dropoffTime = new TimeOnly(pickupDateTime.TimeOfDay.Hours, pickupDateTime.TimeOfDay.Minutes);

                    routes.Add(new Route
                    {
                        RouteId = Convert.ToInt32(row["RouteID"]),
                        PickUpLocationId = Convert.ToInt32(row["PickUpLocationID"]),
                        DropOffLocationId = Convert.ToInt32(row["DropOffLocationID"]),
                        PickUpTime = pickupTime,
                        DropOffTime = dropoffTime,
                        AdditionalDetails = row["ZipCode"].ToString(),
                    });
                }

                // Find the route with the pick-up time closest to the current time
                TimeOnly currentTime = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute);
                currentRoute = routes.OrderBy(route => Math.Abs(route.PickUpTime.CompareTo(currentTime))).FirstOrDefault();
            }

            return currentRoute;
        }
    }
}
