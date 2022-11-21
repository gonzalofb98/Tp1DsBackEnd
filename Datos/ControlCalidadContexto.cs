using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ControlCalidadContexto: DbContext
    {
        public ControlCalidadContexto()
        {

        }

        public ControlCalidadContexto(DbContextOptions<ControlCalidadContexto> opciones): base(opciones)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Color>()
                .ToTable("Colores");

            modelBuilder
                .Entity<Defecto>()
                .ToTable("Defectos");

            modelBuilder
                .Entity<Empleado>()
                .ToTable("Empleados");

            modelBuilder
                .Entity<Incidencia>()
                .ToTable("Incidencias");

            modelBuilder
                .Entity<JornadaLaboral>()
                .ToTable("JornadasLaborales");

            modelBuilder
                .Entity<LineaDeTrabajo>()
                .ToTable("LineasDeTrabajo");

            modelBuilder
                .Entity<Modelo>()
                .ToTable("Modelos");

            modelBuilder
                .Entity<OrdenDeProduccion>()
                .ToTable("OrdenesDeProduccion");

            modelBuilder
                .Entity<Alerta>()
                .ToTable("Alertas");

            modelBuilder
                .Entity<Turno>()
                .ToTable("Turnos");
        }
    }
}
