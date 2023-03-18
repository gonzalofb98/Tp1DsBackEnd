using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class modificarJornadaLaboral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        ALTER TABLE [dbo].[JornadasLaborales]
        DROP CONSTRAINT [FK_JornadasLaborales_OrdenesDeProduccion_OrdenDeProduccionId];

        ALTER TABLE [dbo].[JornadasLaborales]
        ADD CONSTRAINT [FK_JornadasLaborales_OrdenesDeProduccion_OrdenDeProduccionId]
        FOREIGN KEY ([OrdenDeProduccionId]) REFERENCES [dbo].[OrdenesDeProduccion]([Id])
        ON DELETE CASCADE;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        ALTER TABLE [dbo].[JornadasLaborales]
        DROP CONSTRAINT [FK_JornadasLaborales_OrdenesDeProduccion_OrdenDeProduccionId];

        ALTER TABLE [dbo].[JornadasLaborales]
        ADD CONSTRAINT [FK_JornadasLaborales_OrdenesDeProduccion_OrdenDeProduccionId]
        FOREIGN KEY ([OrdenDeProduccionId]) REFERENCES [dbo].[OrdenesDeProduccion]([Id]);
    ");
        }
    }
}
