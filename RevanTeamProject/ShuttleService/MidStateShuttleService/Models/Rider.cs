namespace MidStateShuttleService.Models
{
    public class Rider
    {
        // Primary Key
        public int RiderID { get; set; }

        // Foreign Key to User table
        public int UserId { get; set; }

        // Rider details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

}
