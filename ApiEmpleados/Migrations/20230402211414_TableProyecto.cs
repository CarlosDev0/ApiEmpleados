using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class TableProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProyectoId",
                table: "Registros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProyecto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.ProyectoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registros_ProyectoId",
                table: "Registros",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Proyectos_ProyectoId",
                table: "Registros",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "ProyectoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Proyectos_ProyectoId",
                table: "Registros");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Registros_ProyectoId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "ProyectoId",
                table: "Registros");
        }
    }
}
