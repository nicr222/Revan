using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace MidStateShuttleService.Models
{
    public class CommuncateModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A message is required.")]
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
