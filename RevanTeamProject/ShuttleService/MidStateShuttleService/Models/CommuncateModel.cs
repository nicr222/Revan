using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc.Rendering;
using MidStateShuttleService.Controllers;
using MidStateShuttleService.Models.Data;
using Route = MidStateShuttleService.Models.Data.Route;

namespace MidStateShuttleService.Models
{
    public class CommuncateModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A message is required.")]
        [StringLength(160, ErrorMessage = "This field must not be more than 160 characters.")]
        public string message { get; set; }

        [Required(ErrorMessage = "Please pick message recipiants.")]
        public Route route { get; set; }

        public int PickUpLocationID { get; set; }

        public int DropOffLocationID { get; set; }

        public IEnumerable<SelectListItem> LocationNames { get; set; }
    }
}
