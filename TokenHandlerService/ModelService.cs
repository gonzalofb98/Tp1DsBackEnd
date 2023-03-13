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
    public class ModelService : GenericService<Modelo, ModeloDto>, IModelService
    {
        private readonly IRepositorioGenerico<Modelo> _repositorioGenerico;
        private readonly IMapper _mapper;

        public ModelService(IRepositorioGenerico<Modelo> repositorioGenerico, IMapper mapper) : base(repositorioGenerico, mapper)
        {
            _repositorioGenerico = repositorioGenerico;
            _mapper = mapper;
        }

        public async Task<Modelo> CrearModelo(ModeloDto modeloDto)
        {
            if (modeloDto.Sku == 0)
                throw new ArgumentException("No puede haber un modelo con sku 0");

            var modeloExistente = (await _repositorioGenerico.ListAsync(x => x.Sku == modeloDto.Sku)).FirstOrDefault();

            if (modeloExistente != null)
                throw new ArgumentException("El modelo que intenta crear ya existe");

            var modelo = _mapper.Map<Modelo>(modeloDto);

            await _repositorioGenerico.AgregarAsync(modelo);

            return modelo;
        }

        public async Task EliminarModelo(int id)
        {
            var modeloExistente = await _repositorioGenerico.GetAsync(id);

            if (modeloExistente == null)
                throw new ArgumentException("No se encontró el modelo con el Id: " + id);

            await _repositorioGenerico.DeleteAsync(id);
        }

        public async Task<int> ModificarModelo(int id, ModeloDto modeloDto)
        {
            if (modeloDto.Sku == 0)
                throw new ArgumentException("No puede haber un modelo con SKU 0");

            if (modeloDto == null)
                throw new ArgumentException("No se ingresaron los datos correctamente");
            
            var modeloExistente = await _repositorioGenerico.GetAsync(id);

            if (modeloExistente == null)
                throw new ArgumentException("No se encontró el modelo con el Id: " + id);

            modeloExistente.Sku = modeloDto.Sku;
            modeloExistente.Descripcion = modeloDto.Descripcion;
            modeloExistente.LimiteSuperiorReproceso = modeloDto.LimiteSuperiorReproceso;
            modeloExistente.LimiteInferiorObservado = modeloDto.LimiteInferiorObservado;
            modeloExistente.LimiteInferiorReproceso = modeloDto.LimiteInferiorReproceso;
            modeloExistente.LimiteSuperiorObservado = modeloDto.LimiteSuperiorObservado;

            var response = await _repositorioGenerico.UpdateAsync(modeloExistente);

            return response;
        }
    }
}
