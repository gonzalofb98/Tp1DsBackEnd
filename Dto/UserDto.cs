using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Rol Rol { get; set; }
    }
}
