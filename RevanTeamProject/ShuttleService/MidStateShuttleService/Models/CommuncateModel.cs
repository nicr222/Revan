using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace MidStateShuttleService.Models
{
    public class CommuncateModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A message is required.")]
        [StringLength(160, ErrorMessage = "This field must not be more than 160 characters.")]
        public string message { get; set; }

        [Required(ErrorMessage = "Please pick message recipiants.")]
        public Shuttle[] shuttles { get; set; }
    }

    public class Shuttle
    {
        public int id {  set; get; }

        public bool isSelected { get; set; }
    }
}
