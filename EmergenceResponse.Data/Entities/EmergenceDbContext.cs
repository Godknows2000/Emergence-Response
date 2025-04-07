using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmergenceResponse.Data;

public partial class EmergenceDbContext : DbContext
{
    public EmergenceDbContext()
    {
    }

    public EmergenceDbContext(DbContextOptions<EmergenceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Emergency> Emergencies { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=EmergenceDb;Username=postgres;Password=Godknows");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Emergency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Emergency_pkey");

            entity.ToTable("Emergency");

            entity.HasIndex(e => e.CreatorId, "IX_Emergency_CreatorId");

            entity.HasIndex(e => e.LocationId, "IX_Emergency_LocationId");

            entity.HasIndex(e => e.ServiceProviderId, "IX_Emergency_ServiceProviderId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApprovalDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Creator).WithMany(p => p.Emergencies)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CreatorId_fk");

            entity.HasOne(d => d.Location).WithMany(p => p.Emergencies)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LocationId_fk");

            entity.HasOne(d => d.ServiceProvider).WithMany(p => p.Emergencies)
                .HasForeignKey(d => d.ServiceProviderId)
                .HasConstraintName("ServiceProviderId_fk");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Location_pkey");

            entity.ToTable("Location");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(512);
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(64);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Member_pkey");

            entity.ToTable("Member");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.Forename)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(16);
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(128);
        });

        modelBuilder.Entity<ServiceProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ServiceProvider_pkey");

            entity.ToTable("ServiceProvider");

            entity.HasIndex(e => e.LocationId, "IX_ServiceProvider_LocationId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(512);

            entity.HasOne(d => d.Location).WithMany(p => p.ServiceProviders)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LocationId_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.MemberId, "IX_User_MemberId");

            entity.HasIndex(e => e.ServiceProviderId, "IX_User_ServiceProviderId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthRecoveryCodes).HasMaxLength(512);
            entity.Property(e => e.AuthenticatorKey).HasMaxLength(128);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128);
            entity.Property(e => e.LoginId)
                .IsRequired()
                .HasMaxLength(32);
            entity.Property(e => e.Mobile).HasMaxLength(32);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.SecurityStamp).HasMaxLength(256);

            entity.HasOne(d => d.Member).WithMany(p => p.Users)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("MemberId");

            entity.HasOne(d => d.ServiceProvider).WithMany(p => p.Users)
                .HasForeignKey(d => d.ServiceProviderId)
                .HasConstraintName("ServiceProviderId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
