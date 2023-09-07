﻿// <auto-generated />
using System;
using BTL3_MVT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTL3_MVT.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20230616071950_UpdateDb1")]
    partial class UpdateDb1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BTL3_MVT.Data.Entity.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Citys");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Ethnicity", b =>
                {
                    b.Property<int>("EthnicityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EthnicityId"));

                    b.Property<string>("EthnicityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EthnicityId");

                    b.ToTable("Ethnicity");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("date");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int>("EthnicityId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificAddress")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.Property<int>("WorkCityId")
                        .HasColumnType("int");

                    b.Property<int>("WorkDistrictId")
                        .HasColumnType("int");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.Property<string>("WorkSpecificAddress")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("WorkWardId")
                        .HasColumnType("int");

                    b.HasKey("PersonId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EthnicityId");

                    b.HasIndex("WardId");

                    b.HasIndex("WorkCityId");

                    b.HasIndex("WorkDistrictId");

                    b.HasIndex("WorkId");

                    b.HasIndex("WorkWardId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WardId"));

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WardId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Work", b =>
                {
                    b.Property<int>("WorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkId"));

                    b.Property<string>("WorkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.District", b =>
                {
                    b.HasOne("BTL3_MVT.Data.Entity.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Person", b =>
                {
                    b.HasOne("BTL3_MVT.Data.Entity.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.Ethnicity", "Ethnicity")
                        .WithMany()
                        .HasForeignKey("EthnicityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.City", "WorkCity")
                        .WithMany()
                        .HasForeignKey("WorkCityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.District", "WorkDistrict")
                        .WithMany()
                        .HasForeignKey("WorkDistrictId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BTL3_MVT.Data.Entity.Ward", "WorkWard")
                        .WithMany()
                        .HasForeignKey("WorkWardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Ethnicity");

                    b.Navigation("Ward");

                    b.Navigation("Work");

                    b.Navigation("WorkCity");

                    b.Navigation("WorkDistrict");

                    b.Navigation("WorkWard");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.Ward", b =>
                {
                    b.HasOne("BTL3_MVT.Data.Entity.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("BTL3_MVT.Data.Entity.District", b =>
                {
                    b.Navigation("Wards");
                });
#pragma warning restore 612, 618
        }
    }
}
