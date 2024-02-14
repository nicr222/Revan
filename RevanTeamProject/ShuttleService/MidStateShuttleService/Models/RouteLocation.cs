namespace MidStateShuttleService.Models
{
    public class RouteLocation
    {
        // Primary Key
        public int RouteLocationID { get; set; }

        // Foreign Keys to Route, Location, and NextStop tables
        public int RouteID { get; set; }
        public int LocationID { get; set; }
        public int NextStopID { get; set; }

        // Route location details
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public bool IsStartLocation { get; set; }
    }

}
