using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Turno : EntidadBase
    {
        #region Propiedades
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        #endregion

        #region Relaciones
        public IEnumerable<JornadaLaboral> Jornadas { get; set; }
        #endregion
    }
}
