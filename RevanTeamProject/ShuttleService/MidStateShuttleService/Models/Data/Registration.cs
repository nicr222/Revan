using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Areas.Identity.Data;

namespace MidStateShuttleService.Models.Data;

[Table("Registration")]
public partial class Registration
{
    [Key]
    public int RegistrationId { get; set; }

    public int RouteId { get; set; }

    [StringLength(450)]
    public string? UserId { get; set; }

    [StringLength(20)]
    public string FirstName { get; set; } = null!;

    [StringLength(20)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    public string Phone { get; set; } = null!;

    [StringLength(20)]
    public string PreferContact { get; set; } = null!;

    [StringLength(40)]
    public string Email { get; set; } = null!;

    public bool AgreeToTerms { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SpecialRequestDate { get; set; }

    public bool SepcialRequest { get; set; }

    public bool CheckedIn { get; set; }

    public int? FeedBackId { get; set; }

    [ForeignKey("FeedBackId")]
    [InverseProperty("Registrations")]
    public virtual Feedback? FeedBack { get; set; }

    [ForeignKey("RouteId")]
    [InverseProperty("Registrations")]
    public virtual Route Route { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Registrations")]
    public virtual MidStateShuttleServiceUser? User { get; set; }
}
