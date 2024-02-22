using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models;

[Table("RouteLocation")]
public partial class RouteLocation
{
    [Key]
    [Column("RouteLocationID")]
    public int RouteLocationId { get; set; }

    [Column("RouteID")]
    public int RouteId { get; set; }

    [Column("LocationID")]
    public int LocationId { get; set; }

    [Column("NextStopID")]
    public int NextStopId { get; set; }

    public TimeOnly ArrivalTime { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public bool IsStartLocation { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("RouteLocationLocations")]
    public virtual Location Location { get; set; } = null!;

    [ForeignKey("NextStopId")]
    [InverseProperty("RouteLocationNextStops")]
    public virtual Location NextStop { get; set; } = null!;
}
