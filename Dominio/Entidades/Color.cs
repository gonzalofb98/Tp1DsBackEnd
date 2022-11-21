using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Color : EntidadBase
    {
        public Color()
        {

        }
        public Color(int codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }


        #region Propiedades
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Relaciones
        public IEnumerable<OrdenDeProduccion> OrdenesDeProduccion { get; set; }
        #endregion
    }
}
