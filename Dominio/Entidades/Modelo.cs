using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Modelo : EntidadBase
    {
        public Modelo()
        {

        }
        public Modelo(int sku, string descripcion, int limiteInferiorReproceso, int limiteSuperiorReproceso, int limiteInferiorObservado, int limiteSuperiorObservado)
        {
            Sku = sku;
            Descripcion = descripcion;
            LimiteInferiorReproceso = limiteInferiorReproceso;
            LimiteSuperiorReproceso = limiteSuperiorReproceso;
            LimiteInferiorObservado = limiteInferiorObservado;
            LimiteSuperiorObservado = limiteSuperiorObservado;
        }

        #region Propiedades
        [Required]
        public int Sku { get; set; }

        [Required]
        public string Descripcion { get; set; }
        public int LimiteInferiorReproceso { get; set; }
        public int LimiteSuperiorReproceso { get; set; }
        public int LimiteInferiorObservado { get; set; }

        public int LimiteSuperiorObservado { get; set; }
        #endregion

    }
}
