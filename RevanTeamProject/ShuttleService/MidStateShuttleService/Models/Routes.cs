namespace MidStateShuttleService.Models
{
    public class Routes
    {
        // Primary Key
        public int RouteID { get; set; }

        // Route details
        public int PickLocationID { get; set; }

        public Location PickLocation { get; set; }
        public int DropOffLocationID { get; set; }

        public Location DropOffLocation { get; set; }
        public TimeSpan PickUpTime { get; set; }
        public TimeSpan DropOffTime { get; set; }
    }

}
