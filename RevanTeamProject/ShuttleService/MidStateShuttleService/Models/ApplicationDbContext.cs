using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusDriver> BusDrivers { get; set; }

    public virtual DbSet<BusRider> BusRiders { get; set; }

    public virtual DbSet<BusRoute> BusRoutes { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Rider> Riders { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteLocation> RouteLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Bus__6A0F60B5718116B1");

            entity.HasOne(d => d.BusRider).WithMany(p => p.Buses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bus__BusRiderId__10566F31");

            entity.HasOne(d => d.Driver).WithMany(p => p.Buses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bus__DriverID__48CFD27E");
        });

        modelBuilder.Entity<BusDriver>(entity =>
        {
            entity.HasKey(e => e.BusDriverId).HasName("PK__BusDrive__BA8E141F66849534");

            entity.Property(e => e.BusDriverId).ValueGeneratedNever();

            entity.HasOne(d => d.Bus).WithMany(p => p.BusDrivers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusDriver__BusId__14270015");

            entity.HasOne(d => d.Driver).WithMany(p => p.BusDrivers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusDriver__Drive__151B244E");
        });

        modelBuilder.Entity<BusRider>(entity =>
        {
            entity.HasKey(e => e.BusRiderId).HasName("PK__BusRider__FE19AE6471A5ECDC");

            entity.Property(e => e.BusRiderId).ValueGeneratedNever();

            entity.HasOne(d => d.Bus).WithMany(p => p.BusRiders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusRider__BusID__4BAC3F29");

            entity.HasOne(d => d.Rider).WithMany(p => p.BusRiders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusRider__RiderI__4CA06362");
        });

        modelBuilder.Entity<BusRoute>(entity =>
        {
            entity.HasKey(e => e.BusRouteId).HasName("PK__BusRoute__2A158E8DAE35A2E8");

            entity.HasOne(d => d.Bus).WithMany(p => p.BusRoutes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusRoute__BusId__2A164134");

            entity.HasOne(d => d.Route).WithMany(p => p.BusRoutes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BusRoute__RouteI__2B0A656D");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD049A2B343A");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF69EC0AC18");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__UserID__0D7A0286");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477A8B727BF");

            entity.Property(e => e.State).HasDefaultValue("WI");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.ServiceMessageId).HasName("PK__tmp_ms_x__02DEBA9EF8FCEC20");

            entity.HasOne(d => d.Driver).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__DriverI__1DB06A4F");

            entity.HasOne(d => d.Route).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__RouteID__1CBC4616");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF588103B2104F6");

            entity.HasOne(d => d.FeedBack).WithMany(p => p.Registrations).HasConstraintName("FK__Registrat__FeedB__2739D489");

            entity.HasOne(d => d.Route).WithMany(p => p.Registrations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__Route__2645B050");

            entity.HasOne(d => d.User).WithMany(p => p.Registrations).HasConstraintName("FK__Registrat__UserI__25518C17");
        });

        modelBuilder.Entity<Rider>(entity =>
        {
            entity.HasKey(e => e.RiderId).HasName("PK__Rider__7D726C00670C3598");

            entity.HasOne(d => d.User).WithMany(p => p.Riders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rider__UserId__0E6E26BF");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Route__80979AAD3C88294B");

            entity.HasOne(d => d.DropOffLocation).WithMany(p => p.RouteDropOffLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Route__DropOffLo__19DFD96B");

            entity.HasOne(d => d.PickUpLocation).WithMany(p => p.RoutePickUpLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Route__PickUpLoc__18EBB532");
        });

        modelBuilder.Entity<RouteLocation>(entity =>
        {
            entity.HasKey(e => e.RouteLocationId).HasName("PK__RouteLoc__ADC74A46FA981E29");

            entity.HasOne(d => d.Location).WithMany(p => p.RouteLocationLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteLoca__Locat__1EA48E88");

            entity.HasOne(d => d.NextStop).WithMany(p => p.RouteLocationNextStops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteLoca__NextS__1F98B2C1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
