using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MidStateShuttleService.Models.Data;

namespace MidStateShuttleService.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MidStateShuttleServiceUser class
public class MidStateShuttleServiceUser : IdentityUser
{
    public MidStateShuttleServiceUser()
    {

    }

    [InverseProperty("User")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("User")]
    public virtual ICollection<Rider> Riders { get; set; } = new List<Rider>();

    [InverseProperty("User")]
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}