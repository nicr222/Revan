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

        public int RouteId { get; set; }

        [Required(ErrorMessage = "Pick up location is required.")]
        public int PickUpLocationID { get; set; }

        [Required(ErrorMessage = "Drop location is required.")]
        public int DropOffLocationID { get; set; }

        public IEnumerable<SelectListItem>? LocationNames { get; set; }
    }
}
