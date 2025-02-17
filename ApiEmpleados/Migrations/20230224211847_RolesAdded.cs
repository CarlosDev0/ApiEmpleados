using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_empleados_Users_UserId",
                table: "empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_registros_empleados_empleadoIdEmpleado",
                table: "registros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_registros",
                table: "registros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empleados",
                table: "empleados");

            migrationBuilder.RenameTable(
                name: "registros",
                newName: "Registros");

            migrationBuilder.RenameTable(
                name: "empleados",
                newName: "Empleados");

            migrationBuilder.RenameIndex(
                name: "IX_registros_empleadoIdEmpleado",
                table: "Registros",
                newName: "IX_Registros_empleadoIdEmpleado");

            migrationBuilder.RenameIndex(
                name: "IX_empleados_UserId",
                table: "Empleados",
                newName: "IX_Empleados_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registros",
                table: "Registros",
                column: "IdRegistro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "IdEmpleado");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolName" },
                values: new object[,]
                {
                    { 1, "Empleado" },
                    { 2, "Administrador" },
                    { 3, "Gerente" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Users_UserId",
                table: "Empleados",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Empleados_empleadoIdEmpleado",
                table: "Registros",
                column: "empleadoIdEmpleado",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Users_UserId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Empleados_empleadoIdEmpleado",
                table: "Registros");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registros",
                table: "Registros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.RenameTable(
                name: "Registros",
                newName: "registros");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "empleados");

            migrationBuilder.RenameIndex(
                name: "IX_Registros_empleadoIdEmpleado",
                table: "registros",
                newName: "IX_registros_empleadoIdEmpleado");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_UserId",
                table: "empleados",
                newName: "IX_empleados_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_registros",
                table: "registros",
                column: "IdRegistro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empleados",
                table: "empleados",
                column: "IdEmpleado");

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
    }
}
