﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Photojournal.Data;

#nullable disable

namespace Photojournal.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230131024311_addPhotoAndPhotoblogEntryToDb")]
    partial class addPhotoAndPhotoblogEntryToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Photoblog.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateTaken")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("LocationTakenLat")
                        .HasColumnType("real");

                    b.Property<float?>("LocationTakenLng")
                        .HasColumnType("real");

                    b.Property<int>("PhotoblogEntryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotoblogEntryId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Photoblog.Models.PhotoblogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhotoblogEntries");
                });

            modelBuilder.Entity("Photoblog.Models.Photo", b =>
                {
                    b.HasOne("Photoblog.Models.PhotoblogEntry", "PhotoblogEntry")
                        .WithMany("PhotoList")
                        .HasForeignKey("PhotoblogEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhotoblogEntry");
                });

            modelBuilder.Entity("Photoblog.Models.PhotoblogEntry", b =>
                {
                    b.Navigation("PhotoList");
                });
#pragma warning restore 612, 618
        }
    }
}
