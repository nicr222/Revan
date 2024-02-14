namespace MidStateShuttleService.Models
{
    public class BusRider
    {
        // Primary Key
        public int BusRiderId { get; set; }

        // Foreign Keys
        public int BusID { get; set; }
        public Bus Shuttle { get; set; }

        public int RiderID { get; set; }
        public Rider Rider { get; set; }
    }

}
