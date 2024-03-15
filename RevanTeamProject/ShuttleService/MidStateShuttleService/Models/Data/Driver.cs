using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data
{
    [Table("Driver")]
    public partial class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{10,20}$", ErrorMessage = "Phone number must be between 10 and 20 digits.")]
        public string PhoneNumb { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [InverseProperty("Driver")]
        public virtual ICollection<BusDriver> BusDrivers { get; set; } = new List<BusDriver>();

        [InverseProperty("Driver")]
        public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

        [InverseProperty("Driver")]
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
