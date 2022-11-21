using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Alerta : EntidadBase
    {
        #region Propiedades
        public DateTime FechaLimite { get; set; }
        public DateTime FechaReinicio { get; set; }
        public TipoDefecto Tipo { get; set; }
        #endregion

        #region Relaciones
        public OrdenDeProduccion Orden { get; set; }
        #endregion
    }
}
