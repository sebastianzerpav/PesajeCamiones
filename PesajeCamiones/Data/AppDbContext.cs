using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PesajeCamiones.Data.Models;

namespace PesajeCamiones.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camion> Camiones { get; set; }

    public virtual DbSet<FotoPesaje> FotosPesajes { get; set; }

    public virtual DbSet<Pesaje> Pesajes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camion>(entity =>
        {
            entity.HasKey(e => e.Placa);

            entity.ToTable("Camion");

            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FotoPesaje>(entity =>
        {
            entity.HasKey(e => e.IdFotoPesaje);

            entity.ToTable("FotoPesaje");

            entity.Property(e => e.IdFotoPesaje).HasColumnName("idFotoPesaje");
            entity.Property(e => e.IdPesaje).HasColumnName("idPesaje");
            entity.Property(e => e.ImagenVehiculo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPesajeNavigation).WithMany(p => p.FotoPesajes)
                .HasForeignKey(d => d.IdPesaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FotoPesaje_Pesaje");
        });

        modelBuilder.Entity<Pesaje>(entity =>
        {
            entity.ToTable("Pesaje");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Estacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlacaCamion)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.PlacaCamionNavigation).WithMany(p => p.Pesajes)
                .HasForeignKey(d => d.PlacaCamion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pesaje_Camion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
