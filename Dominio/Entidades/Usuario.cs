using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Usuario : IdentityUser
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public Rol Rol { get; set; }
    }
    public enum Rol
    {
        SUPERVISORLINEA = 1,
        SUPERVISORCALIDAD,
        ADMINISTRADOR
    }
}
