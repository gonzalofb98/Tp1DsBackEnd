using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Empleado : EntidadBase
    {
        #region Propiedades
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set;}
        public string Correo { get; set;}
        public Rol Rol { get; set; }
        #endregion

        public Empleado()
        {

        }
        protected Empleado(int dni, string nombre, string apellido, string correo, Rol rol)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Rol = rol;
        }

        #region Relaciones
        public IEnumerable<OrdenDeProduccion> OrdenesDeProduccion { get; set; }
        public IEnumerable<JornadaLaboral> Jornada { get; set; }
        #endregion
    }
    public enum Rol
    {
        SUPERVISORLINEA,
        SUPERVISORCALIDAD,
        ADMINISTRADOR
    }
}
