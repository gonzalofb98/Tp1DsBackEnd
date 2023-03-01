using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Turno : EntidadBase
    {
        public Turno(DateTime horaInicio, DateTime horaFin, string descripcion)
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Descripcion = descripcion;
        }

        #region Propiedades
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Relaciones
        public IEnumerable<JornadaLaboral> Jornadas { get; set; }
        #endregion
    }
}
