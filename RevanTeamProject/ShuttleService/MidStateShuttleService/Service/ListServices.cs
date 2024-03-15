using MidStateShuttleService.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;

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

        public IEnumerable<Routes> GetRoutes()
        {
            List<Routes> routesList = new List<Routes>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Routes]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    routesList.Add(new Routes
                    {
                        RouteID = Convert.ToInt32(row["RouteID"]),
                        PickUpLocationID = Convert.ToInt32(row["PickUpLocationID"]),
                        DropOffLocationID = Convert.ToInt32(row["DropOffLocationID"]),
                        PickUpTime = TimeSpan.Parse(row["PickUpTime"].ToString()),
                        DropOffTime = TimeSpan.Parse(row["DropOffTime"].ToString()),
                        AdditionalDetails = row["AdditionalDetails"].ToString(),
                        IsArchived = Convert.ToBoolean(row["IsArchived"])
                    });
                }
            }
            return routesList;
        }

        public IEnumerable<Bus> GetBusList()
        {
            List<Bus> busList = new List<Bus>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Bus]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    busList.Add(new Bus
                    {
                        BusId = Convert.ToInt32(row["BusID"]),
                        BusNo = row["BusNumber"].ToString(),
                        PassengerCapacity = Convert.ToInt32(row["PassengerCapacity"]),
                        DriverId = Convert.ToInt32(row["DriverID"]),
                        CurrentRouteId = Convert.ToInt32(row["CurrentRouteID"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),

                    });
                }
            }   
            return busList;
        }

        public IEnumerable<Driver> GetDriverList()
        {
            List<Driver> driverList = new List<Driver>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Driver]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    driverList.Add(new Driver
                    {
                        DriverId = Convert.ToInt32(row["DriverID"]),
                        Name = row["Name"].ToString(),
                        PhoneNumber = row["PhoneNumb"].ToString(),
                        Email = row["Email"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    });
                }
            }
            return driverList;
        }
    }
}
