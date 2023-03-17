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
        public Usuario? SupervisorDeCalidad { get; set; }

        private List<JornadaLaboral?> _jornadas = new List<JornadaLaboral?>();
        public List<JornadaLaboral?> Jornadas { get { return _jornadas; }  set { _jornadas = value; } }

        public List<Alerta>? Alertas { get; set; }
        #endregion

        public OrdenDeProduccion()
        {

        }
        public OrdenDeProduccion(string numero, Modelo modelo,
            Color color, LineaDeTrabajo linea, Usuario supervisorLinea, Turno turno)
        {
            Numero = numero;
            Modelo = modelo;
            Color = color;
            Linea= linea;
            SupervisorDeLinea = supervisorLinea;
            Estado = EstadoOp.ACTIVA;
            FechaInicio = DateTime.Now;
            EstablecerNuevaJornada(turno);
        }

        public void EstablecerNuevaJornada(Turno turno)
        {
            _jornadas.Add(new JornadaLaboral(turno));
        }

        public JornadaLaboral? GetJornadaActual()
        {
            return _jornadas.Last();
        }

        public void FinalizarOrden()
        {
            Estado = EstadoOp.FINALIZADA;
        }
        public void PausarReanudarOrden()
        {
            if (Estado == EstadoOp.PAUSADA) 
                Estado = EstadoOp.ACTIVA;
            else 
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
