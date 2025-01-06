﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250106170621_mig-3")]
    partial class mig3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7333d5e7-ae6a-4523-88ac-7383c9a9f6a5"),
                            CountryId = new Guid("00000000-0000-0000-0000-000000000001"),
                            CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sakarya"
                        });
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.Dealer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adress")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatetorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Dealers");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("24818128-6c4e-453d-8a1f-15f75e8aa746"),
                            CityId = new Guid("7333d5e7-ae6a-4523-88ac-7383c9a9f6a5"),
                            CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Serdivan"
                        });
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatetorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.UserProfileAndDealer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DealerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatetorUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DealerId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserProfileAndDealers");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.City", b =>
                {
                    b.HasOne("SelfWaiter.DealerAPI.Core.Domain.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.Dealer", b =>
                {
                    b.HasOne("SelfWaiter.DealerAPI.Core.Domain.Entities.District", "District")
                        .WithMany("Dealers")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.District", b =>
                {
                    b.HasOne("SelfWaiter.DealerAPI.Core.Domain.Entities.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.UserProfileAndDealer", b =>
                {
                    b.HasOne("SelfWaiter.DealerAPI.Core.Domain.Entities.Dealer", "Dealer")
                        .WithMany("UserProfileAndDealers")
                        .HasForeignKey("DealerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SelfWaiter.DealerAPI.Core.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("UserProfileAndDealers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dealer");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.Dealer", b =>
                {
                    b.Navigation("UserProfileAndDealers");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.District", b =>
                {
                    b.Navigation("Dealers");
                });

            modelBuilder.Entity("SelfWaiter.DealerAPI.Core.Domain.Entities.UserProfile", b =>
                {
                    b.Navigation("UserProfileAndDealers");
                });
#pragma warning restore 612, 618
        }
    }
}
