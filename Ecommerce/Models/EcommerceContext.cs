﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrelli> Carrellis { get; set; }
        public virtual DbSet<CarrelloProdotti> CarrelloProdottis { get; set; }
        public virtual DbSet<Prodotti> Prodottis { get; set; }
        public virtual DbSet<Utenti> Utentis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrelli>(entity =>
            {
                entity.ToTable("Carrelli");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.UtenteId).HasColumnName("UtenteID");

                entity.HasOne(d => d.Utente)
                    .WithMany(p => p.Carrellis)
                    .HasForeignKey(d => d.UtenteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carrelli__Utente__3A81B327");
            });

            modelBuilder.Entity<CarrelloProdotti>(entity =>
            {
                entity.ToTable("CarrelloProdotti");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CarrelloId).HasColumnName("CarrelloID");

                entity.Property(e => e.ProdottoId).HasColumnName("ProdottoID");

                entity.HasOne(d => d.Carrello)
                    .WithMany(p => p.CarrelloProdottis)
                    .HasForeignKey(d => d.CarrelloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarrelloP__Carre__49C3F6B7");

                entity.HasOne(d => d.Prodotto)
                    .WithMany(p => p.CarrelloProdottis)
                    .HasForeignKey(d => d.ProdottoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarrelloP__Prodo__4AB81AF0");
            });

            modelBuilder.Entity<Prodotti>(entity =>
            {
                entity.ToTable("Prodotti");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prezzo).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Utenti>(entity =>
            {
                entity.ToTable("Utenti");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Cognome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}