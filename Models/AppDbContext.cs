using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estacionamiento> Estacionamientos { get; set; }

    public virtual DbSet<Plaza> Plazas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("name=DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<Estacionamiento>(entity =>
        {
            entity.Property(e => e.EstDisponibilidad).HasDefaultValue(true);
        });

        modelBuilder.Entity<Plaza>(entity =>
        {
            entity.Property(e => e.PlazaId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Est).WithMany(p => p.Plazas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plaza_Estacionamiento");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
