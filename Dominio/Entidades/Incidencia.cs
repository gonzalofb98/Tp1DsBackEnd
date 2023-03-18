using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Incidencia : EntidadBase
    {
        #region Propiedades
        [Required]
        public DateTime Fecha { get; set; }
        #endregion

        #region Relaciones
        public Defecto? Defecto { get; set; }
        public Pie? Pie { get; set; }
        public TipoIncidencia Tipo { get; set; } = TipoIncidencia.PRIMERA;
        public JornadaLaboral Jornada { get; set; }
        #endregion

        protected Incidencia()
        {

        }

        public Incidencia(Pie pie, Defecto defecto)
        {
            Fecha = DateTime.Now;
            if (defecto != null) 
            {
                Tipo = TipoIncidencia.DEFECTO;
                Defecto = defecto;
            }
            Pie = pie;
        }
    }

    public enum TipoIncidencia
    {
        PRIMERA,
        DEFECTO
    }
    public enum Pie
    {
        IZQUIERDO,
        DERECHO
    }
}
