using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class _2v010323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
        name: "SupervisorDeCalidadId",
    table: "OrdenesDeProduccion",
    nullable: true,
    oldClrType: typeof(string),
    oldType: "nvarchar(450)",
    oldMaxLength: 450);
            migrationBuilder.AlterColumn<string>(
    name: "SupervisorDeLineaId",
    table: "OrdenesDeProduccion",
    nullable: true,
    oldClrType: typeof(string),
    oldType: "nvarchar(450)",
    oldMaxLength: 450);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
        name: "SupervisorDeCalidadId",
    table: "OrdenesDeProduccion",
    nullable: false,
    oldClrType: typeof(string),
    oldType: "nvarchar(450)",
    oldMaxLength: 450);
            migrationBuilder.AlterColumn<string>(
    name: "SupervisorDeLineaId",
    table: "OrdenesDeProduccion",
    nullable: false,
    oldClrType: typeof(string),
    oldType: "nvarchar(450)",
    oldMaxLength: 450);
        }
    }
}
