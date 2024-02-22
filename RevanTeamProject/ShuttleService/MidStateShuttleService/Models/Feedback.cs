using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Areas.Identity.Data;

namespace MidStateShuttleService.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("FeedbackID")]
    public int FeedbackId { get; set; }

    [StringLength(255)]
    public string Comment { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateSubmitted { get; set; }

    [Column("UserID")]
    [StringLength(450)]
    public string? UserId { get; set; }

    [InverseProperty("FeedBack")]
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [ForeignKey("UserId")]
    [InverseProperty("Feedbacks")]
    public virtual MidStateShuttleServiceUser? User { get; set; }
}
