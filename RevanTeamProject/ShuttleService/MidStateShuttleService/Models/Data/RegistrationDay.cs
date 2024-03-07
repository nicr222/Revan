using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

public partial class RegistrationDay
{
    [Key]
    [Column("RegistrationDayID")]
    public int RegistrationDayId { get; set; }

    [Column("RegistrationID")]
    public int RegistrationId { get; set; }

    [StringLength(10)]
    public string DayOfWeek { get; set; } = null!;

    [ForeignKey("RegistrationId")]
    [InverseProperty("RegistrationDays")]
    public virtual Registration Registration { get; set; } = null!;
}
