using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class ModeloDto
    {
        public int Id { get; set; }
        public int Sku { get; set; }
        public string Descripcion { get; set; }
        public int LimiteInferiorReproceso { get; set; }
        public int LimiteSuperiorReproceso { get; set; }
        public int LimiteInferiorObservado { get; set; }
        public int LimiteSuperiorObservado { get; set; }
    }
}
