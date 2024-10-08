﻿using ApiServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Infrastructure.Database
{
    public class ApiServerContext : DbContext
    {
        public DbSet<ReadingEntity> Reading { get; set; }
        public DbSet<ScaleEntity> Scale { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost;database=ApiServerDB;trusted_connection=true;TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReadingEntity>(entity =>
            {
                entity.HasKey(e => e.ReadId);
                entity.Property(e => e.ReadId).ValueGeneratedOnAdd();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Value).IsRequired().HasColumnType("decimal(18,2)");

                // Kkonfiguracja relacji z użyciem ScaleName jako klucza obcego
                entity.HasOne(r => r.Scale)
                      .WithMany(s => s.Readings)
                      .HasForeignKey(r => r.ScaleName); // Klucz obcy
            });

            modelBuilder.Entity<ScaleEntity>(entity =>
            {
                // Używamy ScaleName jako klucza głównego
                entity.HasKey(e => e.ScaleName);
                entity.Property(e => e.ScaleName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ItemName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SingleItemWeight).IsRequired().HasColumnType("decimal(18,2)");
            });
        }

    }
}
