using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models;

[Table("BusRoute")]
public partial class BusRoute
{
    [Key]
    public int BusRouteId { get; set; }

    public int BusId { get; set; }

    public int RouteId { get; set; }

    [ForeignKey("BusId")]
    [InverseProperty("BusRoutes")]
    public virtual Bus Bus { get; set; } = null!;

    [ForeignKey("RouteId")]
    [InverseProperty("BusRoutes")]
    public virtual Route Route { get; set; } = null!;
}
