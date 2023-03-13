using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LineService : GenericService<LineaDeTrabajo, LineaDeTrabajoDto>, ILineService
    {
        private readonly IRepositorioGenerico<LineaDeTrabajo> _repositorioGenerico;
        private readonly IMapper _mapper;
        public LineService(IRepositorioGenerico<LineaDeTrabajo> repositorioGenerico, IMapper mapper) : base(repositorioGenerico, mapper)
        {
            _repositorioGenerico= repositorioGenerico;
            _mapper= mapper;
        }

        public async Task<LineaDeTrabajo> CrearLineaDeTrabajo(LineaDeTrabajoDto lineaDto)
        {
            if (lineaDto.Numero == 0)
                throw new ArgumentException("No puede haber una línea 0");

            var lineaExistente = (await _repositorioGenerico.ListAsync(x => x.Numero == lineaDto.Numero)).FirstOrDefault();

            if (lineaExistente != null)
                throw new ArgumentException("La línea que intenta crear ya existe");

            var linea = _mapper.Map<LineaDeTrabajo>(lineaDto);

            await _repositorioGenerico.AgregarAsync(linea);

            return linea;
        }

        public async Task<int> ModificarLineaDeTrabajo(int id,LineaDeTrabajoDto lineaDto)
        {
            if (lineaDto.Numero == 0)
                throw new ArgumentException("No existe una línea 0");

            if (lineaDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            var lineaExistente = await _repositorioGenerico.GetAsync(id);

            if (lineaExistente == null)
                throw new ArgumentException("No se encontró la línea de trabajo con el Id: "+id);

            lineaExistente.Numero = lineaDto.Numero;
            lineaExistente.Estado = lineaDto.Estado;

            var response = await _repositorioGenerico.UpdateAsync(lineaExistente);

            return response;
        }

        public async Task EliminarLineaDeTrabajo(int id)
        {
            var lineaExistente = await _repositorioGenerico.GetAsync(id);

            if (lineaExistente == null)
                throw new ArgumentException("No se encontró la línea de trabajo con el Id: " + id);

            await _repositorioGenerico.DeleteAsync(id);
        }
    }
}
