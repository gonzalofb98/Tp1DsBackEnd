using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionDeTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Turnos",
                newName: "HoraInicio");

            migrationBuilder.RenameColumn(
                name: "FechaFin",
                table: "Turnos",
                newName: "HoraFin");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Turnos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Turnos");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "Turnos",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "HoraFin",
                table: "Turnos",
                newName: "FechaFin");
        }
    }
}
