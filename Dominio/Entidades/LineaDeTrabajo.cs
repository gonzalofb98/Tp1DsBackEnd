using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class LineaDeTrabajo : EntidadBase
    {
        public LineaDeTrabajo()
        {

        }

        public LineaDeTrabajo(int numero)
        {
            Numero = numero;
            Estado = EstadoLinea.LIBRE;
        }


        #region Propiedades
        public EstadoLinea Estado { get; set; }

        [Required]
        public int Numero { get; set; }
        #endregion

    }

    public enum EstadoLinea
    {
        LIBRE,
        OCUPADA
    }
}
