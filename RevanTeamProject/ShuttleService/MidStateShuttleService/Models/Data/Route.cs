using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("Route")]
public partial class Route
{
    [Key]
    [Column("RouteID")]
    public int RouteId { get; set; }

    [Column("PickUpLocationID")]
    public int PickUpLocationId { get; set; }

    [Column("DropOffLocationID")]
    public int DropOffLocationId { get; set; }

    public TimeOnly PickUpTime { get; set; }

    public TimeOnly DropOffTime { get; set; }

    [StringLength(300)]
    public string? AdditionalDetails { get; set; }

    public bool IsArchived { get; set; }

    public int BusId { get; set; }

    [ForeignKey("BusId")]
    [InverseProperty("Routes")]
    public virtual Bus Bus { get; set; } = null!;

    [InverseProperty("CurrentRoute")]
    public virtual ICollection<Bus> Buses { get; set; } = new List<Bus>();

    [ForeignKey("DropOffLocationId")]
    [InverseProperty("RouteDropOffLocations")]
    public virtual Location DropOffLocation { get; set; } = null!;

    [InverseProperty("Route")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey("PickUpLocationId")]
    [InverseProperty("RoutePickUpLocations")]
    public virtual Location PickUpLocation { get; set; } = null!;

    [InverseProperty("Route")]
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [InverseProperty("Route")]
    public virtual ICollection<RouteLocation> RouteLocations { get; set; } = new List<RouteLocation>();
}
