using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models
{
    public class Location
    {
        // Primary Key
        public int LocationID { get; set; }

        // Location details
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Abbreviation { get; set; }
    }
}
