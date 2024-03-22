using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidStateShuttleService.Models
{

    // !!**** Temporarily disabled validations, to be addressed in the next sprint. ****!!//
    [Table("Registration")]
    [Index("RouteID", Name = "IX_Registration_RouteId")]
    public partial class RegisterModel
    {
        //[Key]
        //public int RegistrationId { get; set; }

        public int? RouteID { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Must be 10 digits")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(40)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Trip Type is required")]
        [StringLength(10)]
        public string TripType { get; set; }// This could be a dropdown in the UI linked to Types available

        //[Required(ErrorMessage = "Pick Up Location is required")]
        //public string PickUpLocation { get; set; }
        public int? PickUpLocationID { get; set; }

        //[Required(ErrorMessage = "Drop Off Location is required")]
        public int? DropOffLocationID { get; set; }

        //[StringLength(300, ErrorMessage = "Need transportation cannot exceed 300 characters")]
        public string? NeedTransportation { get; set; }

        //[Required(ErrorMessage = "Time is required")]
        public TimeSpan? PickUpTime { get; set; }

        //[Required(ErrorMessage = "Time is required")]
        public TimeSpan? DropOffTime { get; set; }

        //[Required(ErrorMessage = "Special request is required")]
        public bool? SpecialRequest { get; set; } = false; // Assuming this is mandatory for registration

        // New Properties for the Friday request form fields
        //[Required(ErrorMessage = "Arrival Time is required")]
        //[DataType(DataType.Time)]
        public TimeSpan? ArrivalTime { get; set; }

        //[Required(ErrorMessage = "Departure Time is required")]
        //[DataType(DataType.Time)]
        public TimeSpan? DepartureTime { get; set; }

        //[StringLength(300, ErrorMessage = "Which Friday cannot exceed 300 characters")]
        public string? WhichFriday { get; set; }

        //[Required(ErrorMessage = "Friday Trip Type is required")]
        public string? FridayTripType { get; set; }

        [Required]
        public string ContactPreference { get; set; }

        [Required]
        public bool? AgreeTerms { get; set; } = false;//  true/false for agreement

        [Required]
        public bool? FridayAgreeTerms { get; set; } = false;//  true/false for agreement

        public IEnumerable<SelectListItem>? LocationNames { get; set; }

        // Add new properties for route details
        public string? SelectedRouteDetail { get; set; }
        public string? ReturnSelectedRouteDetail { get; set; }

        // New property for selecting days of the week
        public List<string>? SelectedDaysOfWeek { get; set; } = new List<string>();

        public DateOnly? FirstDayExpectingToRide { get; set; }

        public TimeOnly? MustArriveTime { get; set; }

        public TimeOnly? CanLeaveTime { get; set; }


        public TimeOnly? FridayMustArriveTime { get; set; }

        public TimeOnly? FridayCanLeaveTime { get; set; }

        public string? SpecialPickUpLocation { get; set; }

        public string? SpecialDropOffLocation { get; set; }

        [ForeignKey("RouteID")]
        public virtual Routes? Route { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        //[Required(ErrorMessage = "Pick Up Location is required")]
        public int? FridayPickUpLocationID { get; set; }

        //[Required(ErrorMessage = "Drop Off Location is required")]
        public int? FridayDropOffLocationID { get; set; }
    }

}
