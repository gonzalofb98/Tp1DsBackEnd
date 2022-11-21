using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Defecto : EntidadBase
    {
        public Defecto()
        {

        }
        public Defecto(string descripcion, TipoDefecto tipo)
        {
            Descripcion = descripcion;
            Tipo = tipo;
        }


        #region Propiedades
        public string Descripcion { get; set; }
        #endregion

        #region Relaciones
        public TipoDefecto Tipo { get; set; }
        #endregion
    }

    public enum TipoDefecto
    {
        REPROCESO = 0,
        OBSERVADO
    }
}
