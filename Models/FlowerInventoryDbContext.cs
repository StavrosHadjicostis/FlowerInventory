using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlowerInventory.Models;

public partial class FlowerInventoryDbContext : DbContext
{
    public FlowerInventoryDbContext()
    {
    }

    public FlowerInventoryDbContext(DbContextOptions<FlowerInventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Flower> Flowers { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         var config = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile("appsettings.json")
    //             .Build();

    //         var connectionString = config.GetConnectionString("FlowerInventoryDB");
    //         optionsBuilder.UseSqlServer(connectionString);
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0750991776");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flowers__3214EC072292B437");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Flowers)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Flowers__Categor__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
