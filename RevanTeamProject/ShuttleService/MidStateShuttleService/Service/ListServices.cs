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

        // For use in the Communication View. Only returns the Id of the bus to populate the shuttle select.
        // Uses the shuttle model in the communicationmodel as opposed to the bus model for the IsSelected field.
        public IEnumerable<Shuttle> GetShuttleList()
        {
            List<Shuttle> shuttleList = new List<Shuttle>();

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataTable dataTable = new DataTable();

                string sql = "SELECT * FROM [Bus]";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    shuttleList.Add(new Shuttle
                    {
                        id = Convert.ToInt32(row["BusID"])
                    });
                }
            }

            return shuttleList;
        }
    }
}
