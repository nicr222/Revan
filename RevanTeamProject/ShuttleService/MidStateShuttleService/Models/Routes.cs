using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidStateShuttleService.Models
{
    [Table("Route")]
    [Index("DropOffLocationID", Name = "IX_Route_DropOffLocationID")]
    [Index("PickUpLocationID", Name = "IX_Route_PickUpLocationID")]
    public class Routes
    {
        [Key]
        public int RouteID { get; set; }

        [Display(Name = "Pick Up Location")]
        [Required(ErrorMessage = "Please select a pick-up location.")]
        public int PickUpLocationID { get; set; }

        [Display(Name = "Drop Off Location")]
        [Required(ErrorMessage = "Please select a drop-off location.")]
        public int DropOffLocationID { get; set; }

        [Display(Name = "Pick Up Time")]
        [Required(ErrorMessage = "Please enter a valid pick-up time.")]
        [DataType(DataType.Time)]
        [RegularExpression("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Please enter a valid time.")]
        public TimeSpan? PickUpTime { get; set; }

        [Display(Name = "Drop Off Time")]
        [Required(ErrorMessage = "Please enter a valid drop-off time.")]
        [DataType(DataType.Time)]
        [RegularExpression("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Please enter a valid time.")]
        public TimeSpan? DropOffTime { get; set; }

        [Display(Name = "Additional Details")]
        [StringLength(500, ErrorMessage = "Additional details cannot exceed 500 characters.")]
        [RegularExpression("^[a-zA-Z0-9.,!?'\";:@#$%^&*()_+=\\-\\/]*$", ErrorMessage = "Additional details can only contain letters, numbers, and important special characters.")]
        public string AdditionalDetails { get; set; }

        [DefaultValue(false)]
        public bool IsArchived { get; set; }

        public int BusId { get; set; }

        [ForeignKey("PickUpLocationID")]
        public virtual ICollection<Location> PickUpLocation { get; set; } = new List<Location>();

        [ForeignKey("DropOffLocationID")]
        public virtual ICollection<Location> DropOffLocation { get; set; } = new List<Location>();
    }
}
