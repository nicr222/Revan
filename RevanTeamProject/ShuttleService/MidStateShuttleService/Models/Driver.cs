using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models
{
    [Table("Driver")]
    public partial class Driver
    {
        [Key]
        public int DriverId { get; set; }

        
        [StringLength(100)]
        public string Name { get; set; }

        
        [StringLength(20)]
        [RegularExpression(@"^\d{10,20}$", ErrorMessage = "Phone number must be between 10 and 20 digits.")]
        public string PhoneNumber { get; set; }

        
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string ToStringDriver()
        {
            return Name;
        }
    }
}
