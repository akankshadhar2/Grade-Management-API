﻿// <auto-generated />
using System;
using GradeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GradeManagement.Migrations
{
    [DbContext(typeof(GradeDbContext))]
    partial class GradeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GradeManagement.Models.Domain.Center", b =>
                {
                    b.Property<Guid>("CenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CenterId");

                    b.ToTable("Centers");
                });

            modelBuilder.Entity("GradeManagement.Models.Domain.Grade", b =>
                {
                    b.Property<Guid>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradeId");

                    b.HasIndex("CenterId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("GradeManagement.Models.Domain.Grade", b =>
                {
                    b.HasOne("GradeManagement.Models.Domain.Center", "Center")
                        .WithMany("Grades")
                        .HasForeignKey("CenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Center");
                });

            modelBuilder.Entity("GradeManagement.Models.Domain.Center", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
