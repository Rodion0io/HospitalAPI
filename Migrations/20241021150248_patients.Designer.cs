﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using hospital_api.Dates;

#nullable disable

namespace hospital_api.Migrations
{
    [DbContext(typeof(AccountsContext))]
    [Migration("20241021150248_patients")]
    partial class patients
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("hospital_api.Modules.BlackListTokens", b =>
                {
                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.Property<string>("doctorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("token");

                    b.ToTable("BlackListTokens");
                });

            modelBuilder.Entity("hospital_api.Modules.Doctor", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("gender")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phone")
                        .HasColumnType("text");

                    b.Property<Guid>("speciality")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("hospital_api.Modules.PatientModel", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("gender")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("hospital_api.Modules.SpecialityModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Specialities");
                });
#pragma warning restore 612, 618
        }
    }
}
