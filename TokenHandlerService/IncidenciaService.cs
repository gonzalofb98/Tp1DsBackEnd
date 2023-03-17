using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Services.Herramientas;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IncidenciaService : GenericService<Incidencia, IncidenciaDto>, IIncidenciaService
    {
        private readonly IRepositorioGenerico<Incidencia> _repositorioIncidencias;
        private readonly IRepositorioGenerico<OrdenDeProduccion> _repositorioOrdenes;
        private readonly IRepositorioGenerico<Defecto> _repositorioDefecto;
        
        private readonly IMapper _mapper;

        public IncidenciaService(IRepositorioGenerico<Incidencia> repositorioIncidencias, 
            IRepositorioGenerico<OrdenDeProduccion> repositorioOrdenes, 
            IRepositorioGenerico<Defecto> repositorioDefecto,
            IMapper mapper) 
            : base(repositorioIncidencias, mapper)
        {
            _repositorioIncidencias = repositorioIncidencias;
            _repositorioOrdenes = repositorioOrdenes;
            _repositorioDefecto = repositorioDefecto;
            _mapper = mapper;
        }

        public async Task<int> CrearIncidencia(string nroOp,IncidenciaDto incidenciaDto)
        {
            if (incidenciaDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            if (nroOp == "")
                throw new ArgumentException("No se ingreso el numero de la OP a la que corresponde la incidencia");

            var opActual = (await _repositorioOrdenes.GetConFiltro(x => x.Numero == nroOp)).LastOrDefault();

            if (opActual == null)
                throw new ArgumentException("El numero de OP ingresado es incorrecto");

            if (incidenciaDto.Defecto.Equals(""))
                throw new ArgumentException("No se específico el defecto");

            var defecto = (await _repositorioDefecto.GetConFiltro(x=> x.Descripcion == incidenciaDto.Defecto)).LastOrDefault();

            if (defecto == null)
                throw new ArgumentException("Defecto no encontrado");

            if ( incidenciaDto.Pie < (Pie) 0 ||  incidenciaDto.Pie > (Pie) 1)
                throw new ArgumentException("No se específico en que pie se encontro el defecto");

            if ( incidenciaDto.Tipo < (TipoIncidencia) 0 ||  incidenciaDto.Tipo > (TipoIncidencia) 1)
                throw new ArgumentException("No se específico el tipo de incidencia");

            var incidencia = new Incidencia(incidenciaDto.Pie, defecto);

            opActual.Jornadas = Utils.SPGetJornadas(opActual.Id);
            var jornadaActual = opActual.GetJornadaActual();

            if (jornadaActual == null)
                throw new ArgumentException("No se encontro la jornada Actual");
            jornadaActual.AgregarIncidencia(incidencia);
            return await _repositorioIncidencias.AgregarAsync(incidencia);

        }

        public Task EliminarIncidencia(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Incidencia>> GetByDefecto(string defecto)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModificarIncidencia(int id, IncidenciaDto incidenciaDto)
        {
            throw new NotImplementedException();
        }
    }
}
