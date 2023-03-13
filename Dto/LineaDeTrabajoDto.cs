using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class LineaDeTrabajoDto
    {
        public EstadoLinea Estado { get; set; }

        public int Numero { get; set; }

        public int Id { get; set; }
    }
}
