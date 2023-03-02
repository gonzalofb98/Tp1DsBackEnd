using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorDeCalidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeCalidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeCalidadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesDeProduccion_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion");

            migrationBuilder.DropColumn(
                name: "SupervisorDeCalidadId",
                table: "OrdenesDeProduccion");
        }
    }
}
