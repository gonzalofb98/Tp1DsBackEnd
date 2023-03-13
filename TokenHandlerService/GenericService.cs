using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GenericService<Entity, Response> : IGenericService<Entity, Response>
        where Entity: EntidadBase 
        where Response : class
    {
        private readonly IRepositorioGenerico<Entity> _genericRepository;
        private readonly IMapper _mapper;

        public GenericService(IRepositorioGenerico<Entity> repositorioGenerico, IMapper mapper)
        {
            _genericRepository = repositorioGenerico;
            _mapper = mapper;
        }

        public async Task<int> AgregarAsync(Entity item)
        {
            var entityModel = _mapper.Map<Entity>(item);
            return await _genericRepository.AgregarAsync(entityModel);
        }

        public async Task DeleteAsync(int id)
        {
             await _genericRepository.DeleteAsync(id);
        }

        public async Task DeleteRange(IEnumerable<Entity> elements)
        {
            await  _genericRepository.DeleteRange(_mapper.Map<IEnumerable<Entity>, ICollection<Entity>>(elements));
        }

        public async Task<Response> GetAsync(int id)
        {
            var entity = await _genericRepository.GetAsync(id);

            if (entity == null)
                return null;

            return _mapper.Map<Response>(entity);
        }

        public async Task<ICollection<Response>> GetConFiltro(Expression<Func<Entity, bool>> predicado)
        {
            var collection = await _genericRepository.GetConFiltro(predicado);
            return _mapper.Map<ICollection<Entity>, ICollection<Response>>(collection);
        }

        public async Task<ICollection<Entity>> GetTodosAsync()
        {
            var collection = await _genericRepository.GetTodosAsync();
            return collection;
        }

        public async Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate, params string[] includes)
        {
            var collection = await _genericRepository.ListAsync(predicate, includes);
            return collection;
        }

        public async Task<List<Entity>> ListAsync(params string[] includes)
        {
            var collection = await _genericRepository.ListAsync(includes);
            return collection;
        }

        public async Task<int> UpdateAsync(Entity item)
        {
            var entityModel = _mapper.Map<Entity>(item);
            return await _genericRepository.UpdateAsync(entityModel);
        }
    }
}
