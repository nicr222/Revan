using System.ComponentModel.DataAnnotations;

namespace MidStateShuttleService.Models
{
    public class CommuncateModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A message is required.")]
        public string message { get; set; }

        [Required(ErrorMessage = "Please pick message recipiants.")]
        public Array shuttles { get; set; }
    }
}
