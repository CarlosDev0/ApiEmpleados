using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class RegistroEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Empleados_empleadoIdEmpleado",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_empleadoIdEmpleado",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "empleadoIdEmpleado",
                table: "Registros");

            migrationBuilder.RenameColumn(
                name: "IdEmpleado",
                table: "Registros",
                newName: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_EmpleadoId",
                table: "Registros",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Empleados_EmpleadoId",
                table: "Registros",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Empleados_EmpleadoId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_EmpleadoId",
                table: "Registros");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "Registros",
                newName: "IdEmpleado");

            migrationBuilder.AddColumn<Guid>(
                name: "empleadoIdEmpleado",
                table: "Registros",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_empleadoIdEmpleado",
                table: "Registros",
                column: "empleadoIdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Empleados_empleadoIdEmpleado",
                table: "Registros",
                column: "empleadoIdEmpleado",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }
    }
}
