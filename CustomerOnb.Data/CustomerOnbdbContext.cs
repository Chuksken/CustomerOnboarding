﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerOnb.Data
{
    public partial class CustomerOnbdbContext : DbContext
    {
        public CustomerOnbdbContext()
        {
        }

        public CustomerOnbdbContext(DbContextOptions<CustomerOnbdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Lga> Lga { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<TokenTable> TokenTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LgaId).HasColumnName("LgaID");

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.Lga)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.LgaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_LGA");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_State");
            });

            modelBuilder.Entity<Lga>(entity =>
            {
                entity.ToTable("LGA");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Lga)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LGA_State");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TokenTable>(entity =>
            {
                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.TokenExpiration).HasColumnType("datetime");

                entity.Property(e => e.TokenGeneratedTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}