using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[Required]
        //public bool IsFirstTimeUsingShuttle { get; set; }

        [Required]
        public string TripType { get; set; }


        [Required]
        public string PickUpLocation { get; set; }

        [Required]
        public string DropOffLocation { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

    }
    
}

