using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Areas.Identity.Data;

namespace MidStateShuttleService.Models;

[Table("Rider")]
public partial class Rider
{
    [Key]
    [Column("RiderID")]
    public int RiderId { get; set; }

    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("Rider")]
    public virtual ICollection<BusRider> BusRiders { get; set; } = new List<BusRider>();

    [InverseProperty("BusRider")]
    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    [ForeignKey("UserId")]
    [InverseProperty("Riders")]
    public virtual MidStateShuttleServiceUser User { get; set; } = null!;
}
