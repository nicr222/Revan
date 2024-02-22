using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("BusRider")]
public partial class BusRider
{
    [Key]
    public int BusRiderId { get; set; }

    [Column("BusID")]
    public int BusId { get; set; }

    [Column("RiderID")]
    public int RiderId { get; set; }

    [ForeignKey("BusId")]
    [InverseProperty("BusRiders")]
    public virtual Bus Bus { get; set; } = null!;

    [ForeignKey("RiderId")]
    [InverseProperty("BusRiders")]
    public virtual Rider Rider { get; set; } = null!;
}
