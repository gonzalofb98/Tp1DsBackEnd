using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class OrdenDeProduccionDto
    {
        public int id { get; set; }
        public string nroOp { get; set; }
        public int modeloId { get; set; }
        public int colorId { get; set; }
        public int lineaId { get; set; }
        public string email { get; set; }
    }
}
