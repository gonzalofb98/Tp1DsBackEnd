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
    public class ColorService : GenericService<Color, ColorDto>, IColorService
    {
        private readonly IRepositorioGenerico<Color> _repositorioGenerico;
        private readonly IMapper _mapper;

        public ColorService(IRepositorioGenerico<Color> repositorioGenerico, IMapper mapper) : base(repositorioGenerico, mapper)
        {
            _repositorioGenerico = repositorioGenerico;
            _mapper = mapper;
        }

        public async Task<Color> CrearColor(ColorDto colorDto)
        {
            if (colorDto.Codigo == 0)
                throw new ArgumentException("No puede haber un color con codigo 0");

            var colorExistente = (await _repositorioGenerico.ListAsync(x => x.Codigo == colorDto.Codigo)).FirstOrDefault();

            if (colorExistente != null)
                throw new ArgumentException("El color que intenta crear ya existe");

            var color = _mapper.Map<Color>(colorDto);

            await _repositorioGenerico.AgregarAsync(color);

            return color;
        }

        public async Task EliminarColor(int id)
        {
            var colorExistente = await _repositorioGenerico.GetAsync(id);

            if (colorExistente == null)
                throw new ArgumentException("No se encontró el modelo con el Id: " + id);

            await _repositorioGenerico.DeleteAsync(id);
        }

        public async Task<int> ModificarColor(int id, ColorDto colorDto)
        {
            if (colorDto.Codigo == 0)
                throw new ArgumentException("No puede haber un color con codigo 0");

            if (colorDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");

            var colorExistente = await _repositorioGenerico.GetAsync(id);

            if (colorExistente == null)
                throw new ArgumentException("No se encontró el color con el Id: " + id);

            colorExistente.Codigo = colorDto.Codigo;
            colorExistente.Descripcion = colorDto.Descripcion;

            var response = await _repositorioGenerico.UpdateAsync(colorExistente);

            return response;
        }
    }
}
