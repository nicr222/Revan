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
    public int DriverId { get; set; }

    public int BusRiderId { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Bus")]
    public virtual ICollection<BusDriver> BusDrivers { get; set; } = new List<BusDriver>();

    [ForeignKey("BusRiderId")]
    [InverseProperty("Buses")]
    public virtual Rider BusRider { get; set; } = null!;

    [InverseProperty("Bus")]
    public virtual ICollection<BusRider> BusRiders { get; set; } = new List<BusRider>();

    [InverseProperty("Bus")]
    public virtual ICollection<BusRoute> BusRoutes { get; set; } = new List<BusRoute>();

    [ForeignKey("DriverId")]
    [InverseProperty("Buses")]
    public virtual Driver Driver { get; set; } = null!;
}
