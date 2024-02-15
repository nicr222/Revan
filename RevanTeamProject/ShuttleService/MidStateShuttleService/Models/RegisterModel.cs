using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class RegisterModel
    {
        public int RiderID { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Must be 10 digits")]
        //public long StudentId { get; set; } // Changed from int to long
        public int? UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Trip Type is required")]
        public string TripType { get; set; }// This could be a dropdown in the UI linked to Types available

        [Required(ErrorMessage = "Pick Up Location is required")]
        //public string PickUpLocation { get; set; }
        public int PickLocationID { get; set; }

        [Required(ErrorMessage = "Drop Off Location is required")]
        //public string DropOffLocation { get; set; }
        public int DropOffLocationID { get; set; }

        [StringLength(300, ErrorMessage = "Need transportation cannot exceed 300 characters")]
        public string NeedTransportation { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public DateTime PickUpTime { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public DateTime DropOffTime { get; set; }

        [Required(ErrorMessage = "Special request is required")]
        public bool? SpecialRequest { get; set; } // Assuming this is mandatory for registration

        // New Properties for the Friday request form fields
        [Required(ErrorMessage = "Arrival Time is required")]
        [DataType(DataType.Time)]
        public TimeSpan? ArrivalTime { get; set; }

        [Required(ErrorMessage = "Departure Time is required")]
        [DataType(DataType.Time)]
        public TimeSpan? DepartureTime { get; set; }

        [StringLength(300, ErrorMessage = "Which Friday cannot exceed 300 characters")]
        public string? WhichFriday { get; set; }

        [Required(ErrorMessage = "Friday Trip Type is required")]
        public string? FridayTripType { get; set; }

        [Required]
        public string ContactPreference { get; set; }

        [Required]
        public bool? AgreeTerms { get; set; } //  true/false for agreement
    }

}
