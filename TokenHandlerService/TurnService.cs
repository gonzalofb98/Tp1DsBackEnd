using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TurnService : GenericService<Turno, TurnoDto>, ITurnService
    {
        private readonly IRepositorioGenerico<Turno> _repositorioGenerico;
        private readonly IMapper _mapper;

        public TurnService(IRepositorioGenerico<Turno> repositorioGenerico, IMapper mapper) : base(repositorioGenerico, mapper)
        {
            _repositorioGenerico = repositorioGenerico;
            _mapper = mapper;
        }

        public async Task<Turno> CrearTurno(TurnoDto turnoDto)
        {
            if (turnoDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            if (turnoDto.Descripcion.Equals(""))
                throw new ArgumentException("No puede haber un Turno sin descripción");

            if (turnoDto.HoraInicio.Equals(""))
                throw new ArgumentException("No puede haber una hora de inicio vacia");

            if (turnoDto.HoraFin.Equals(""))
                throw new ArgumentException("No puede haber un hora de fin vacia");

            var turnoExistente = (await _repositorioGenerico.GetConFiltro(x => x.Descripcion == turnoDto.Descripcion)).FirstOrDefault();

            if (turnoExistente != null)
                throw new ArgumentException("El Turno que intenta crear ya existe");

            var turno = _mapper.Map<Turno>(turnoDto);
            
            await _repositorioGenerico.AgregarAsync(turno);
            return turno;
        }

        public async Task EliminarTurno(int id)
        {
            var turnoExistente = await _repositorioGenerico.GetAsync(id);

            if (turnoExistente == null)
                throw new ArgumentException("No se encontró el Turno con el Id: " + id);

            await _repositorioGenerico.DeleteAsync(id);
        }

        public async Task<List<int>> GetTotalHorasByDescripcion(string desc)
        {
            if (desc == null || desc == "") 
                throw new ArgumentException("Error, ingrese una descripcion ");

            var turno = (await _repositorioGenerico.ListAsync(x => x.Descripcion == desc)).FirstOrDefault();

            if (turno == null)
                throw new ArgumentException("Error, el turno no existe");

            var listaHoras = turno.getTotalHoras();
            return listaHoras;
        }

        public async Task<int> ModificarTurno(int id, TurnoDto turnoDto)
        {
            if (turnoDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            if (turnoDto.Descripcion.Equals(""))
                throw new ArgumentException("No puede haber un Turno sin descripción");

            if (turnoDto.HoraInicio.Equals(""))
                throw new ArgumentException("No puede haber una hora de inicio vacia");

            if (turnoDto.HoraFin.Equals(""))
                throw new ArgumentException("No puede haber un hora de fin vacia");

            var turnoExistente = await _repositorioGenerico.GetAsync(id);

            if (turnoExistente == null)
                throw new ArgumentException("El turno con id= " + id + " no existe");
            
            DateTime horaInicio;
            DateTime horaFin;

            if (!DateTime.TryParseExact(turnoDto.HoraInicio, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaInicio) || 
                !DateTime.TryParseExact(turnoDto.HoraFin, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaFin))
                throw new ArgumentException("Error, los datos de Hora Inicio y Hora Fin no estan en el formato correcto");

            turnoExistente.HoraFin = horaFin;
            turnoExistente.HoraInicio = horaInicio;
            turnoExistente.Descripcion = turnoDto.Descripcion;

            var response = await _repositorioGenerico.UpdateAsync(turnoExistente);
            return response;
        }
    }
}
