using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineasDeTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasDeTrabajo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteInferiorReproceso = table.Column<int>(type: "int", nullable: false),
                    LimiteSuperiorReproceso = table.Column<int>(type: "int", nullable: false),
                    LimiteInferiorObservado = table.Column<int>(type: "int", nullable: false),
                    LimiteSuperiorObservado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeProduccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    LineaId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    SupervisorDeLineaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeProduccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Empleados_SupervisorDeLineaId",
                        column: x => x.SupervisorDeLineaId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_LineasDeTrabajo_LineaId",
                        column: x => x.LineaId,
                        principalTable: "LineasDeTrabajo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeProduccion_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaReinicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    OrdenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_OrdenesDeProduccion_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "OrdenesDeProduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JornadasLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    SupervisorDeCalidadId = table.Column<int>(type: "int", nullable: false),
                    OrdenDeProduccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JornadasLaborales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JornadasLaborales_Empleados_SupervisorDeCalidadId",
                        column: x => x.SupervisorDeCalidadId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JornadasLaborales_OrdenesDeProduccion_OrdenDeProduccionId",
                        column: x => x.OrdenDeProduccionId,
                        principalTable: "OrdenesDeProduccion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JornadasLaborales_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefectoId = table.Column<int>(type: "int", nullable: true),
                    Pie = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    JornadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Defectos_DefectoId",
                        column: x => x.DefectoId,
                        principalTable: "Defectos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidencias_JornadasLaborales_JornadaId",
                        column: x => x.JornadaId,
                        principalTable: "JornadasLaborales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_OrdenId",
                table: "Alertas",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_DefectoId",
                table: "Incidencias",
                column: "DefectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_JornadaId",
                table: "Incidencias",
                column: "JornadaId");

            migrationBuilder.CreateIndex(
                name: "IX_JornadasLaborales_OrdenDeProduccionId",
                table: "JornadasLaborales",
                column: "OrdenDeProduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_JornadasLaborales_SupervisorDeCalidadId",
                table: "JornadasLaborales",
                column: "SupervisorDeCalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_JornadasLaborales_TurnoId",
                table: "JornadasLaborales",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_ColorId",
                table: "OrdenesDeProduccion",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_LineaId",
                table: "OrdenesDeProduccion",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_ModeloId",
                table: "OrdenesDeProduccion",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeProduccion_SupervisorDeLineaId",
                table: "OrdenesDeProduccion",
                column: "SupervisorDeLineaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "Defectos");

            migrationBuilder.DropTable(
                name: "JornadasLaborales");

            migrationBuilder.DropTable(
                name: "OrdenesDeProduccion");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "LineasDeTrabajo");

            migrationBuilder.DropTable(
                name: "Modelos");
        }
    }
}
