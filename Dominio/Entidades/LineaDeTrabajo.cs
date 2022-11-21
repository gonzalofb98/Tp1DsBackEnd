using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class LineaDeTrabajo : EntidadBase
    {
        public EstadoLinea estado = EstadoLinea.LIBRE;
        public LineaDeTrabajo()
        {

        }
        public LineaDeTrabajo(int numero)
        {
            Numero = numero;
        }


        #region Propiedades
        public int Numero { get; set; }
        #endregion

        #region Relaciones
        public IEnumerable<OrdenDeProduccion> OrdenesDeProduccion { get; set; }
        #endregion
    }

    public enum EstadoLinea
    {
        LIBRE,
        OCUPADA
    }
}
