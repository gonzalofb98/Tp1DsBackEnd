using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class JornadaLaboral : EntidadBase
    {
        #region Propiedades
        [Required]
        public DateTime HoraInicio { get; set; }

        [Required]
        public DateTime HoraFin { get; set; }
        public List<Incidencia> Incidencias { get; set; } = new List<Incidencia>();
        public Turno Turno { get; set; }
        #endregion

        public JornadaLaboral()
        {

        }

        public JornadaLaboral(DateTime fechaInicio, Turno turno)
        {
            HoraInicio = fechaInicio;
            Turno = turno;
        }
        public JornadaLaboral(DateTime fechaInicio, DateTime fechaFin, Turno turno)
        {
            HoraInicio = fechaInicio;
            HoraFin = fechaFin;
            Turno = turno;
        }

        public void AgregarIncidencia(DateTime fecha, Pie? pie = null, Defecto? defecto = null)
        {
            Incidencias.Add(new Incidencia(fecha, pie, defecto));
        }
    }
}
