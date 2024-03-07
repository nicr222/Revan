using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MidStateShuttleService.Models.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<RegistrationDay> RegistrationDays { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RouteLocation> RouteLocations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=0515_392_v1_Revan;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Bus__6A0F60B5718116B1");

            entity.HasOne(d => d.CurrentRoute).WithMany(p => p.Buses).HasConstraintName("FK__Bus__CurrentRout__4E53A1AA");

            entity.HasOne(d => d.Driver).WithMany(p => p.Buses).HasConstraintName("FK__Bus__DriverID__4D5F7D71");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD049A2B343A");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF69EC0AC18");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks).HasConstraintName("UserId");
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
        });

        modelBuilder.Entity<RegistrationDay>(entity =>
        {
            entity.HasKey(e => e.RegistrationDayId).HasName("PK__Registra__F8B74C8E0554913D");

            entity.HasOne(d => d.Registration).WithMany(p => p.RegistrationDays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__Regis__5AB9788F");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Route__80979AAD3C88294B");

            entity.HasOne(d => d.Bus).WithMany(p => p.Routes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Route__BusId__55009F39");

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

            entity.HasOne(d => d.Route).WithMany(p => p.RouteLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RouteLoca__Route__57DD0BE4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C2781EAC2");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
