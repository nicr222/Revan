using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models;

[Table("BusDriver")]
public partial class BusDriver
{
    [Key]
    public int BusDriverId { get; set; }

    public int BusId { get; set; }

    public int DriverId { get; set; }

    [ForeignKey("BusId")]
    [InverseProperty("BusDrivers")]
    public virtual Bus Bus { get; set; } = null!;

    [ForeignKey("DriverId")]
    [InverseProperty("BusDrivers")]
    public virtual Driver Driver { get; set; } = null!;
}
