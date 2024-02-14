namespace MidStateShuttleService.Models
{
    public class ReservationModel
    {
        // Primary Key
        public int ReservationID { get; set; }

        // Foreign Keys to Rider and Routes tables
        public int? RiderID { get; set; }
        public int? RouteID { get; set; }

        // Reservation details
        public int? CheckedIn { get; set; }

        public DateTime? Date { get; set; }
        public bool SpeicalRequest { get; set; }
    }
}
