using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("Message")]
public partial class Message
{
    [Key]
    [Column("ServiceMessageID")]
    public int ServiceMessageId { get; set; }

    [Column("RouteID")]
    public int RouteId { get; set; }

    [Column("DriverID")]
    public int DriverId { get; set; }

    [Column("Message")]
    [StringLength(155)]
    public string Message1 { get; set; } = null!;

    [ForeignKey("DriverId")]
    [InverseProperty("Messages")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("RouteId")]
    [InverseProperty("Messages")]
    public virtual Route Route { get; set; } = null!;
}
