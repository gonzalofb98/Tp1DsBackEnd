using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class modificacionIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JornadasLaborales_AspNetUsers_SupervisorDeCalidadId",
                table: "JornadasLaborales");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion");

            migrationBuilder.DropIndex(
                name: "IX_JornadasLaborales_SupervisorDeCalidadId",
                table: "JornadasLaborales");

            migrationBuilder.DropColumn(
                name: "SupervisorDeCalidadId",
                table: "JornadasLaborales");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeCalidadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorDeCalidadId",
                table: "JornadasLaborales",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JornadasLaborales_SupervisorDeCalidadId",
                table: "JornadasLaborales",
                column: "SupervisorDeCalidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_JornadasLaborales_AspNetUsers_SupervisorDeCalidadId",
                table: "JornadasLaborales",
                column: "SupervisorDeCalidadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeProduccion_AspNetUsers_SupervisorDeCalidadId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeCalidadId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
