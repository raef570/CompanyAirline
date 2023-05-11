using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyAirline.Models.Company;

public partial class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pilote> Pilotes { get; set; }

    public virtual DbSet<Vol> Vols { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CompanyCS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pilote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pilote__3214EC071F1D14DC");

            entity.ToTable("Pilote");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Nationality)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Vol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vol__3214EC07566CEFEB");

            entity.ToTable("Vol");

            entity.Property(e => e.Avion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PiloteId).HasColumnName("PiloteId");
            entity.Property(e => e.Tarif)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VilleArrive)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Ville_Arrive");
            entity.Property(e => e.VilleDepart)
                .HasMaxLength(50)
                .HasColumnName("Ville_Depart");

            entity.HasOne(d => d.Pilote).WithMany(p => p.Vols)
                .HasForeignKey(d => d.PiloteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vol_ToTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
