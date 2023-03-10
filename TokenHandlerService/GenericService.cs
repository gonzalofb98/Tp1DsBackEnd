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
    public class GenericService<Entity, Response, Request> : IGenericService<Entity, Response, Request>
        where Entity: EntidadBase 
        where Response : class
    {
        private readonly IRepositorioGenerico<Entity> _genericRepository;

        public GenericService(IRepositorioGenerico<Entity> repositorioGenerico)
        {
            _genericRepository = repositorioGenerico;
        }

        public Task<int> AgregarAsync(Entity item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRange(IEnumerable<Entity> elements)
        {
            throw new NotImplementedException();
        }

        public Task<Entity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Entity>> GetConFiltro(Expression<Func<Entity, bool>> predicado)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Entity>> GetTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Entity>> ListAsync(Expression<Func<Entity, bool>> predicate, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entity>> ListAsync(params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Entity item)
        {
            throw new NotImplementedException();
        }
    }
}
