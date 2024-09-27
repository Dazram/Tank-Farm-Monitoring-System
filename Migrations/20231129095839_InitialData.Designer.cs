﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFMS.Data;

#nullable disable

namespace Capstone.Migrations
{
    [DbContext(typeof(TFMSDBContext))]
    [Migration("20231129095839_InitialData")]
    partial class InitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Capstone.Models.Loading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Customer")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriverName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TruckNo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Loading");
                });

            modelBuilder.Entity("Capstone.Models.Offloading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriverName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Supplier")
                        .HasColumnType("TEXT");

                    b.Property<string>("TruckNo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Offloading");
                });

            modelBuilder.Entity("Capstone.Models.StorageTank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StorageTank");
                });

            modelBuilder.Entity("Capstone.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
