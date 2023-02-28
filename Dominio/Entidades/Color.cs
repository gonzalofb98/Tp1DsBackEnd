using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Color : EntidadBase
    {
        public Color()
        {

        }
        public Color(int codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }


        #region Propiedades
        [Required]
        public int Codigo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        #endregion
    }
}
