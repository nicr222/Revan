namespace MidStateShuttleService.Models
{
    public class Reservation
    {
        // Change private properties to public properties with get;set;
        public int? Id { get; private set; }
        public int StudentID { get; private set; }
        public int? ShuttleNumber { get; set; }
        public string? PickUpLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }

        // Add a parameterless constructor
        public Reservation() { }

        public Reservation(int id, int studentID, int shuttleNumber, string pickUpLocation, string dropOffLocation, DateOnly date, TimeOnly time)
        {
            Id = id;
            StudentID = studentID;
            ShuttleNumber = shuttleNumber;
            PickUpLocation = pickUpLocation;
            DropOffLocation = dropOffLocation;
            Date = date;
            Time = time;
        }
    }
}
