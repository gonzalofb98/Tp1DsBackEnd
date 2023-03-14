using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DefectService : GenericService<Defecto, DefectoDto>, IDefectService
    {
        private readonly IRepositorioGenerico<Defecto> _repositorioGenerico;
        private readonly IMapper _mapper;

        public DefectService(IRepositorioGenerico<Defecto> repositorioGenerico, IMapper mapper) : base(repositorioGenerico, mapper)
        {
            _repositorioGenerico = repositorioGenerico;
            _mapper = mapper;
        }

        public async Task<Defecto> CrearDefecto(DefectoDto defectoDto)
        {
            if (defectoDto.Descripcion.Equals(""))
                throw new ArgumentException("No se ingreso una descripción");

            var defectoExistente = (await _repositorioGenerico.ListAsync(x => x.Descripcion == defectoDto.Descripcion)).FirstOrDefault();

            if (defectoExistente != null)
                throw new ArgumentException("El defecto que intenta crear ya existe");

            var defecto = _mapper.Map<Defecto>(defectoDto);

            await _repositorioGenerico.AgregarAsync(defecto);

            return defecto;
        }

        public async Task EliminarDefecto(int id)
        {
            var defectoExistente = await _repositorioGenerico.GetAsync(id);

            if (defectoExistente == null)
                throw new ArgumentException("No se encontró el defecto con el Id: " + id);

            await _repositorioGenerico.DeleteAsync(id);
        }

        public async Task<int> ModificarDefecto(int id, DefectoDto defectoDto)
        {
            if (defectoDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            if (defectoDto.Descripcion.Equals(""))
                throw new ArgumentException("No se ingreso una descripción");

            var defectoExistente = await _repositorioGenerico.GetAsync(id);

            if (defectoExistente == null)
                throw new ArgumentException("No se encontró el defecto con el Id: " + id);

            defectoExistente.Tipo = defectoDto.Tipo;
            defectoExistente.Descripcion = defectoDto.Descripcion;

            var response = await _repositorioGenerico.UpdateAsync(defectoExistente);

            return response;
        }
    }
}
