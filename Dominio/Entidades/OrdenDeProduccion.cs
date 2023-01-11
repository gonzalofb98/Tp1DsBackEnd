using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class OrdenDeProduccion : EntidadBase
    {
        #region Propiedades
        [Required]
        public string Numero { get; set; }
        [Required]
        public DateTime FechaInicio  { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        #endregion

        #region Relaciones
        public Modelo Modelo { get; set; }
        public Color Color { get; set; }
        public LineaDeTrabajo? Linea { get; set; }
        public EstadoOp Estado { get; set; }
        public Usuario SupervisorDeLinea { get; set; }

        private List<JornadaLaboral?> _jornadas = new List<JornadaLaboral?>();
        public List<JornadaLaboral?> Jornadas { get { return _jornadas; } }

        //CAMBIAR ESTO
        public List<Alerta>? Alertas { get; set; }
        #endregion

        protected OrdenDeProduccion()
        {

        }

        public OrdenDeProduccion(string numero, DateTime fechaInicio, Modelo modelo, 
            Color color, LineaDeTrabajo linea, Usuario supervisor)
        {
            Numero = numero;
            FechaInicio = fechaInicio;
            Modelo = modelo;
            Color = color;
            if(linea.Estado.Equals(EstadoLinea.LIBRE)) Linea = linea;
            SupervisorDeLinea = supervisor;
            Estado = EstadoOp.ACTIVA;
        }

        public void EstablecerNuevaJornada(DateTime fechaInicio, DateTime fechaFin, Turno turno)
        {
            _jornadas.Add(new JornadaLaboral(fechaInicio,fechaFin,turno));
        }

        public JornadaLaboral? GetJornadaActual()
        {
            return _jornadas.FirstOrDefault();
        }

        public void FinalizarOrden()
        {
            Estado = EstadoOp.FINALIZADA;
        }
        public void PausarOrden()
        {
            Estado = EstadoOp.PAUSADA;
        }
    }
    public enum EstadoOp
    {
        ACTIVA,
        PAUSADA,
        FINALIZADA,
    }
}
