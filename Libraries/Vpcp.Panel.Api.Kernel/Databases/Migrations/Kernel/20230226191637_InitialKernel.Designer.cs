﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vpcp.Kernel.Databases.Contexts;

#nullable disable

namespace Vpcp.Panel.Api.Kernel.Databases.Migrations.Kernel
{
    [DbContext(typeof(KernelContext))]
    [Migration("20230226191637_InitialKernel")]
    partial class InitialKernel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Vpcp.Kernel.Models.Entities.Admin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Key")
                        .IsUnique();

                    b.ToTable("Admins", (string)null);

                    b.HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");
                });

            modelBuilder.Entity("Vpcp.Kernel.Models.Entities.Dictionary", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasAnnotation("MySQL:Charset", "utf8mb4")
                        .HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");

                    b.HasKey("Id");

                    b.HasIndex("Word")
                        .IsUnique();

                    b.ToTable("Dictionaries", (string)null);

                    b.HasAnnotation("MySQL:Collation", "utf8mb4_general_ci");
                });
#pragma warning restore 612, 618
        }
    }
}