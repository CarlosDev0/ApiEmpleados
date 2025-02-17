using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class AddStastusEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Empleados",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empleados");
        }
    }
}
