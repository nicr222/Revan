namespace MidStateShuttleService.Models
{
    public class Feedback
    {
        // Primary Key
        public int FeedbackID { get; set; }

        // Foreign Key to User table
        public int RiderId { get; set; }

        public Rider Rider { get; set; }

        // Feedback details
        public string Comment { get; set; }
        public DateTime DateSubmitted { get; set; }
    }

}
