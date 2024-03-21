namespace MidStateShuttleService.Models
{
    public class AllModels
    {
        public IEnumerable<CommuncateModel> Communcate { get; set; }

        public IEnumerable<Location> Location { get; set; }

        public Message Message { get; set; }

        public RegisterModel Register { get; set; }

        //public ReservationModel Reservation { get; set; }

        public IEnumerable<Routes> Route { get; set; }

        public IEnumerable<Driver> Driver { get; set; }
    }
}
