﻿// <auto-generated />
using System;
using ApiEmpleados.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiEmpleados.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230223202256_User")]
    partial class User
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiEmpleados.Models.Empleado", b =>
                {
                    b.Property<Guid>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEmpleado");

                    b.ToTable("empleados");
                });

            modelBuilder.Entity("ApiEmpleados.Models.Registro", b =>
                {
                    b.Property<int>("IdRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRegistro"));

                    b.Property<DateTime>("Fin")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdEmpleado")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.HasKey("IdRegistro");

                    b.ToTable("registros");
                });

            modelBuilder.Entity("ApiEmpleados.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
