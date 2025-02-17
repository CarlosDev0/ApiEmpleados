using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class EmpleadoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "empleadoIdEmpleado",
                table: "registros",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_registros_empleadoIdEmpleado",
                table: "registros",
                column: "empleadoIdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_empleados_UserId",
                table: "empleados",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_empleados_Users_UserId",
                table: "empleados",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registros_empleados_empleadoIdEmpleado",
                table: "registros",
                column: "empleadoIdEmpleado",
                principalTable: "empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_empleados_Users_UserId",
                table: "empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_registros_empleados_empleadoIdEmpleado",
                table: "registros");

            migrationBuilder.DropIndex(
                name: "IX_registros_empleadoIdEmpleado",
                table: "registros");

            migrationBuilder.DropIndex(
                name: "IX_empleados_UserId",
                table: "empleados");

            migrationBuilder.DropColumn(
                name: "empleadoIdEmpleado",
                table: "registros");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "empleados");
        }
    }
}
