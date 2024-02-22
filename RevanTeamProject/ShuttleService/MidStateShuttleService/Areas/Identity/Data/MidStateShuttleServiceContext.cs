using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Areas.Identity.Data;
using MidStateShuttleService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace MidStateShuttleService.Data;

public class MidStateShuttleServiceContext : IdentityDbContext<MidStateShuttleServiceUser>
{
    public MidStateShuttleServiceContext(DbContextOptions<MidStateShuttleServiceContext> options)
        : base(options)
    {
    }

    protected MidStateShuttleServiceContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Registration>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Registrations).HasConstraintName("FK__Registrat__UserI__25518C17");
        });

        builder.Entity<Feedback>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__UserID__0D7A0286");
        });

        builder.Entity<Rider>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Riders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rider__UserId__0E6E26BF");
        });
    }
}
