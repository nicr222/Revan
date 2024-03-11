using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class Message
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A message is required.")]
        [StringLength(160, ErrorMessage = "This field must not be more than 50 characters.")]
        [RegularExpression("^[A-Za-z\\s]{2,}$", ErrorMessage = "Must contain only characters and be at least 2 characters long")]
        public string name { get; set; }

        [Required(ErrorMessage = "A message is required.")]
        [StringLength(160, ErrorMessage = "This field must not be more than 160 characters.")]
        public string message { get; set; }

        public bool responseRequired { get; set; }

        public string? contactInfo { get; set; }
    }
}
