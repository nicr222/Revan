namespace MidStateShuttleService.Models
{
    public class Bus
    {
        public class Shuttle
        {
            // Primary Key
            public int BusId { get; set; }

            // Shuttle details
            public string BusNo { get; set; }
            public int PassengerCapacity { get; set; }

            // Foreign Key to Driver table
            public int DriverID { get; set; }
            public Driver Driver { get; set; }

            // Foreign Key to BusRider table
            public int RiderId { get; set; }
            public BusRider BusRider { get; set; }
        }

    }
}
