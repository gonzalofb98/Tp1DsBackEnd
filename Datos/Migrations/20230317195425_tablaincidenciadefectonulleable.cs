using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class tablaincidenciadefectonulleable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidencias_Defectos_DefectoId",
                table: "Incidencias");

            migrationBuilder.AlterColumn<int>(
                name: "DefectoId",
                table: "Incidencias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidencias_Defectos_DefectoId",
                table: "Incidencias",
                column: "DefectoId",
                principalTable: "Defectos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidencias_Defectos_DefectoId",
                table: "Incidencias");

            migrationBuilder.AlterColumn<int>(
                name: "DefectoId",
                table: "Incidencias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidencias_Defectos_DefectoId",
                table: "Incidencias",
                column: "DefectoId",
                principalTable: "Defectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
