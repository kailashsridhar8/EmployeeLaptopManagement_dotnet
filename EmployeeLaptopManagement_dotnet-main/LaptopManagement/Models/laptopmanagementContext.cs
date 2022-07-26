﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LaptopManagement.Models
{
    public partial class laptopmanagementContext : DbContext
    {
        public laptopmanagementContext()
        {
        }

        public laptopmanagementContext(DbContextOptions<laptopmanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InstalledSoftware> InstalledSoftwares { get; set; } = null!;
        public virtual DbSet<Laptop> Laptops { get; set; } = null!;
        public virtual DbSet<Software> Softwares { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=1234;database=laptopmanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<InstalledSoftware>(entity =>
            {
                entity.ToTable("installed_softwares");

                entity.HasIndex(e => e.LaptopId, "laptop_id_idx");

                entity.HasIndex(e => e.SoftwareId, "software_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LaptopId).HasColumnName("laptop_id");

                entity.Property(e => e.SoftwareId).HasColumnName("software_id");

                entity.HasOne(d => d.Laptop)
                    .WithMany(p => p.InstalledSoftwares)
                    .HasForeignKey(d => d.LaptopId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("laptop_id");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.InstalledSoftwares)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("software_id");
            });

            modelBuilder.Entity<Laptop>(entity =>
            {
                entity.ToTable("laptops");

                entity.HasIndex(e => e.UserId, "user_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Processor)
                    .HasMaxLength(45)
                    .HasColumnName("processor");

                entity.Property(e => e.Storage)
                    .HasMaxLength(45)
                    .HasColumnName("storage");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Laptops)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.ToTable("softwares");

                entity.HasIndex(e => e.Id, "software_code_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(45)
                    .HasColumnName("emailId");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasDefaultValueSql("'0'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
