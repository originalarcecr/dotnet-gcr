﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi.GeographyAPI.Data;

#nullable disable

namespace TodoApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240826112857_InitialCreate3")]
    partial class InitialCreate3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Barrio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DistritoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("DistritoId");

                    b.ToTable("Barrios");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Canton", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProvinciaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Cantones");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Distrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CantonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CantonId");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Distritos");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Barrio", b =>
                {
                    b.HasOne("TodoApi.GeographyAPI.Models.Distrito", "Distrito")
                        .WithMany()
                        .HasForeignKey("DistritoId");

                    b.Navigation("Distrito");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Canton", b =>
                {
                    b.HasOne("TodoApi.GeographyAPI.Models.Provincia", "Provincia")
                        .WithMany()
                        .HasForeignKey("ProvinciaId");

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("TodoApi.GeographyAPI.Models.Distrito", b =>
                {
                    b.HasOne("TodoApi.GeographyAPI.Models.Canton", "Canton")
                        .WithMany()
                        .HasForeignKey("CantonId");

                    b.Navigation("Canton");
                });
#pragma warning restore 612, 618
        }
    }
}
