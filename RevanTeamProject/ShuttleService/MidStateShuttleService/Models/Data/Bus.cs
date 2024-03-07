using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

[Table("Bus")]
public partial class Bus
{
    [Key]
    public int BusId { get; set; }

    [StringLength(50)]
    public string BusNo { get; set; } = null!;

    public int PassengerCapacity { get; set; }

    [Column("DriverID")]
    public int? DriverId { get; set; }

    public bool IsActive { get; set; }

    public int? CurrentRouteId { get; set; }

    [ForeignKey("CurrentRouteId")]
    [InverseProperty("Buses")]
    public virtual Route? CurrentRoute { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Buses")]
    public virtual Driver? Driver { get; set; }

    [InverseProperty("Bus")]
    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
}
