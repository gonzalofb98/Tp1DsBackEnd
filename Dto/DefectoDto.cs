using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DefectoDto
    {
        public string Descripcion { get; set; }

        public TipoDefecto Tipo { get; set; }
    }
}
