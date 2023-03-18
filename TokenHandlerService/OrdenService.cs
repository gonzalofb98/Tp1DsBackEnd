using AutoMapper;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Identity;
using Services.Herramientas;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrdenService : GenericService<OrdenDeProduccion, OrdenDeProduccionDto>, IOrdenService
    {
        private readonly IRepositorioGenerico<OrdenDeProduccion> _repositorioOrdenDeProduccion;
        private readonly IRepositorioGenerico<Color> _repositorioColor;
        private readonly IRepositorioGenerico<Modelo> _repositorioModelo;
        private readonly IRepositorioGenerico<LineaDeTrabajo> _repositorioLineaDeTrabajo;
        private readonly IRepositorioGenerico<Turno> _repositorioTurno;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;

        public OrdenService(
            IRepositorioGenerico<OrdenDeProduccion> repositorioOrdenDeProduccion, 
            IRepositorioGenerico<Color> repositorioColor, 
            IRepositorioGenerico<Modelo> repositorioModelo, 
            IRepositorioGenerico<LineaDeTrabajo> repositorioLineaDeTrabajo,
            IRepositorioGenerico<Turno> repositorioTurno,
            UserManager<Usuario> userManager,
            IMapper mapper
            ) : base(repositorioOrdenDeProduccion, mapper)
        {
            _repositorioOrdenDeProduccion = repositorioOrdenDeProduccion;
            _repositorioColor = repositorioColor;
            _repositorioModelo = repositorioModelo;
            _repositorioLineaDeTrabajo = repositorioLineaDeTrabajo;
            _repositorioTurno = repositorioTurno;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CrearOrden(OrdenDeProduccionDto ordenDeProduccionDto)
        {
            if (ordenDeProduccionDto.nroOp == "") 
                throw new ArgumentException("No se ingreso el numero de OP");

            var opExistente = (await _repositorioOrdenDeProduccion.ListAsync(x => x.Numero == ordenDeProduccionDto.nroOp)).FirstOrDefault();
            if (opExistente != null)
                throw new ArgumentException("El Numero de OP ya existe");
            
            var modelo = await _repositorioModelo.GetAsync(ordenDeProduccionDto.modeloId);
            if (modelo == null) 
                throw new ArgumentException("El modelo no existe");

            var color = await _repositorioColor.GetAsync(ordenDeProduccionDto.colorId);
            if (color == null) 
                throw new ArgumentException("El color no existe");

            var linea = await _repositorioLineaDeTrabajo.GetAsync(ordenDeProduccionDto.lineaId);
            if (linea == null) 
                throw new ArgumentException("La línea no existe");
            linea.Estado = EstadoLinea.OCUPADA;

            var usuario = await _userManager.FindByEmailAsync(ordenDeProduccionDto.email);
            if (usuario == null) 
                throw new ArgumentException("El usuario no existe");

            var turnos = (await _repositorioTurno.GetTodosAsync()).ToList();
            var turnoActual = Utils.GetTurnoActual(turnos);

            var newOp = new OrdenDeProduccion(ordenDeProduccionDto.nroOp, modelo, color, linea, usuario, turnoActual);
            return await _repositorioOrdenDeProduccion.AgregarAsync(newOp);
        }

        public Task EliminarOrden(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModificarOrden(int id, OrdenDeProduccionDto ordenDeProduccionDto)
        {
            throw new NotImplementedException();
        }
    }
}
