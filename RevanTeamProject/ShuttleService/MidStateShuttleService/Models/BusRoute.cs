namespace MidStateShuttleService.Models
{
    public class BusRoute
    {
        // Primary Key
        public int BusRouteID { get; set; }

        // Foreign Keys to Bus and Route tables
        public int BusID { get; set; }
        public Bus Bus { get; set; }

        public int RouteID { get; set; }

        public Routes Route { get; set; }
    }

}
