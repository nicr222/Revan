using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("Location")]
public partial class Location
{
    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [StringLength(100)]
    public string City { get; set; } = null!;

    [StringLength(2)]
    public string State { get; set; } = null!;

    [StringLength(10)]
    public string ZipCode { get; set; } = null!;

    [StringLength(5)]
    public string Abbreviation { get; set; } = null!;

    [InverseProperty("DropOffLocation")]
    public virtual ICollection<Route> RouteDropOffLocations { get; set; } = new List<Route>();

    [InverseProperty("Location")]
    public virtual ICollection<RouteLocation> RouteLocationLocations { get; set; } = new List<RouteLocation>();

    [InverseProperty("NextStop")]
    public virtual ICollection<RouteLocation> RouteLocationNextStops { get; set; } = new List<RouteLocation>();

    [InverseProperty("PickUpLocation")]
    public virtual ICollection<Route> RoutePickUpLocations { get; set; } = new List<Route>();
}
