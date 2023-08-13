﻿// <auto-generated />
using System;
using Business.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Business.Migrations
{
    [DbContext(typeof(RContext))]
    partial class RContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Model.Classes.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("company");

                    b.Property<string>("LocationDetail")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("location_detail");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("Id");

                    b.ToTable("contact", (string)null);
                });

            modelBuilder.Entity("Model.Classes.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("detail");

                    b.Property<bool>("IsReady")
                        .HasColumnType("boolean")
                        .HasColumnName("isready");

                    b.Property<string>("ReportSituation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("report_situation");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp(6)")
                        .HasColumnName("request_date");

                    b.HasKey("Id");

                    b.ToTable("report", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}