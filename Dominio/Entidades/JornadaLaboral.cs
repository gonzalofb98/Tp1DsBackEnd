using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class JornadaLaboral : EntidadBase
    {
        #region Propiedades
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Incidencia> Incidencias { get; set; } = new List<Incidencia>();
        public Turno Turno { get; set; }
        #endregion

        public JornadaLaboral()
        {

        }

        public JornadaLaboral(DateTime fechaInicio, Turno turno)
        {
            FechaInicio = fechaInicio;
            Turno = turno;
        }
        public JornadaLaboral(DateTime fechaInicio, DateTime fechaFin, Turno turno)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Turno = turno;
        }

        #region Relaciones
        public Empleado SupervisorDeCalidad { get; set; }
        #endregion

        public void AgregarIncidencia(DateTime fecha, Pie? pie = null, Defecto? defecto = null)
        {
            Incidencias.Add(new Incidencia(fecha, pie, defecto));
        }
    }
}
