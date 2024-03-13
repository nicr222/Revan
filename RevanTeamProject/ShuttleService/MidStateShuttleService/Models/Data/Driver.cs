using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("Driver")]
public partial class Driver
{
    [Key]
    public int DriverId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumb { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<BusDriver> BusDrivers { get; set; } = new List<BusDriver>();

    [InverseProperty("Driver")]
    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    [InverseProperty("Driver")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
