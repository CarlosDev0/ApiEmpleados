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
    [Migration("20230225013514_Roles-Users")]
    partial class RolesUsers
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdEmpleado");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("ApiEmpleados.Models.Registro", b =>
                {
                    b.Property<int>("IdRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRegistro"));

                    b.Property<Guid>("EmpleadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Fin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.HasKey("IdRegistro");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Registros");
                });

            modelBuilder.Entity("ApiEmpleados.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RolName = "Empleado"
                        },
                        new
                        {
                            Id = 2,
                            RolName = "Administrador"
                        },
                        new
                        {
                            Id = 3,
                            RolName = "Gerente"
                        });
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

            modelBuilder.Entity("RolUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RolUser");
                });

            modelBuilder.Entity("ApiEmpleados.Models.Empleado", b =>
                {
                    b.HasOne("ApiEmpleados.Models.User", "User")
                        .WithOne("Empleado")
                        .HasForeignKey("ApiEmpleados.Models.Empleado", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiEmpleados.Models.Registro", b =>
                {
                    b.HasOne("ApiEmpleados.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("RolUser", b =>
                {
                    b.HasOne("ApiEmpleados.Models.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiEmpleados.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiEmpleados.Models.User", b =>
                {
                    b.Navigation("Empleado");
                });
#pragma warning restore 612, 618
        }
    }
}
