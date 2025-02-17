using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class DateTime_In_Registro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "registros");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Inicio",
                table: "registros",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fin",
                table: "registros",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Inicio",
                table: "registros",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Fin",
                table: "registros",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Fecha",
                table: "registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
