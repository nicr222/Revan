using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class Location
    {
        // Primary Key
        public int LocationID { get; set; }

        // Location details
        [Required(ErrorMessage = "Please enter the name of the location.")]
        [RegularExpression("^[A-Za-z\\s]{2,25}$", ErrorMessage = "Name must contain only characters, be at least 2 characters long, and not exceed 25 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the address.")]
        [RegularExpression("^[A-Za-z0-9\\s]{2,50}$", ErrorMessage = "Address must contain only characters and numbers, be at least 2 characters long, and not exceed 50 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the city.")]
        [RegularExpression("^[A-Za-z\\s]{2,50}$", ErrorMessage = "City must contain only characters, be at least 2 characters long, and not exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the state.")]
        [StringLength(2, ErrorMessage = "State should be a maximum of 2 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the zip code.")]
        [RegularExpression("^[0-9]{5,10}$", ErrorMessage = "Zip code must contain only numbers, be at least 5 digits long, and not exceed 10 digits.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter the abbreviation.")]
        [RegularExpression("^[A-Za-z]{3,3}$", ErrorMessage = "Abbreviation must contain only characters and be exactly 3 characters long.")]
        public string Abbreviation { get; set; }
    }
}
