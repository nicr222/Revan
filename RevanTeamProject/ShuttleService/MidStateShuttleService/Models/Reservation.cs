namespace MidStateShuttleService.Models
{
    public class Reservation
    {
        private int id { get; }
        private int studentID { get; }
        public int ShuttleNumber;
        public string PickUpLocation;
        public string DropOffLocation;
        public DateOnly Date;
        public TimeOnly Time;

        public Reservation(int id, int studentID, int shuttleNumber, string pickUpLocation, string dropOffLocation, DateOnly date, TimeOnly time)
        {
            this.id = id;
            this.studentID = studentID;
            this.ShuttleNumber = shuttleNumber;
            this.PickUpLocation = pickUpLocation;
            this.DropOffLocation = dropOffLocation;
            this.Date = date;
            this.Time = time;
        }
    }
}
