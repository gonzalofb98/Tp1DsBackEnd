using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Datos.Migrations;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorOrdenDeProduccion : ControllerBase
    {
        private readonly IRepositorioGenerico<OrdenDeProduccion> _repositorio;
        private readonly IRepositorioGenerico<Modelo> _repositorioModelo;
        private readonly IRepositorioGenerico<Color> _repositorioColor;
        private readonly IRepositorioGenerico<LineaDeTrabajo> _repositorioLinea;
        private readonly UserManager<Usuario> _userManager;

        public ControladorOrdenDeProduccion(
            IRepositorioGenerico<OrdenDeProduccion> repositorio,
            IRepositorioGenerico<Modelo> repositorioModelo,
            IRepositorioGenerico<Color> repositorioColor,
            IRepositorioGenerico<LineaDeTrabajo> repositorioLinea,
            UserManager<Usuario> userManager
            )
        {
            this._repositorio = repositorio;
            this._repositorioLinea= repositorioLinea;
            this._repositorioModelo = repositorioModelo;
            this._repositorioColor = repositorioColor;
            this._userManager = userManager;
        }


        #region Metodos Get
        [HttpGet("OrdenesDeProduccion")]
        public async Task<IActionResult> GetOps()
        {
            var ops = await _repositorio.ListAsync("Modelo", "Color", "Linea", "SupervisorDeLinea");
            return Ok(ops);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetOpById(int id)
        {
            return Ok(await _repositorio.GetAsync(id));
        }

        [HttpGet("ByUsuario")]
        public async Task<IActionResult> GetOpByUsuario(string email)
        {
            return Ok((await _repositorio.ListAsync(x => ((x.Estado != EstadoOp.FINALIZADA) && (x.SupervisorDeLinea.Email == email)), "Modelo", "Color", "Linea", "SupervisorDeLinea")).FirstOrDefault());
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("ChangeEstado")]
        public async Task<IActionResult> ChangeEstadoOp(string email)
        {
            var opExistente = (await _repositorio.ListAsync(x => ((x.Estado != EstadoOp.FINALIZADA) && (x.SupervisorDeLinea.Email == email)), "Modelo", "Color", "Linea", "SupervisorDeLinea")).FirstOrDefault();
            if (opExistente.Estado == EstadoOp.ACTIVA)
            {
                opExistente.Estado = EstadoOp.PAUSADA;
            }
            else
            {
                opExistente.Estado = EstadoOp.ACTIVA;
            }

            await _repositorio.UpdateAsync(opExistente);

            return Ok(opExistente);
        }

        [HttpPut("FinishOp")]
        public async Task<IActionResult> FinishOp(string email)
        {
            var opExistente = (await _repositorio.ListAsync(x => ((x.Estado != EstadoOp.FINALIZADA) && (x.SupervisorDeLinea.Email == email)), "Modelo", "Color", "Linea", "SupervisorDeLinea")).FirstOrDefault();
            if (opExistente != null || opExistente.Estado != EstadoOp.FINALIZADA)
            {
                opExistente.Estado = EstadoOp.FINALIZADA;
                var linea = (await _repositorioLinea.GetConFiltro(x => x.Numero == opExistente.Linea.Numero)).FirstOrDefault();
                linea.Estado = EstadoLinea.LIBRE;
                opExistente.Linea.Estado = EstadoLinea.LIBRE;
                await _repositorio.UpdateAsync(opExistente);
                await _repositorioLinea.UpdateAsync(linea);
            return Ok("La Op se finalizo correctamente.");
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveOp([FromBody] OrdenDto opDto)
        {
            if (opDto.nroOp == "") return BadRequest("No se ingreso el numero de OP");
            var c = await _repositorio.GetConFiltro(x => x.Numero == opDto.nroOp);
            if (c.Count != 0)
            {
                return BadRequest("El Numero de OP ya existe");
            }
            else
            {
                try
                {
                    //Cambiar por servicios con mapper
                    var modelo = await _repositorioModelo.GetAsync(opDto.modeloId);
                    if (modelo == null) return BadRequest("El modelo no existe");

                    var color = await _repositorioColor.GetAsync(opDto.colorId);
                    if (color == null) return BadRequest("El color no existe");

                    var linea = await _repositorioLinea.GetAsync(opDto.lineaId);
                    if (linea == null) return BadRequest("La línea no existe");
                    linea.Estado = EstadoLinea.OCUPADA;

                    var usuario = await _userManager.FindByEmailAsync(opDto.email);
                    if (usuario == null) return BadRequest("El usuario no existe");
                    await _repositorio.AgregarAsync(new OrdenDeProduccion(opDto.nroOp, modelo, color, linea, usuario));

                    return Created("", "Created");
                }
                catch
                {
                    return BadRequest("Error al crear la OP");
                }

            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOpById(int id)
        {
            var op = (await _repositorio.ListAsync((x) => x.Id == id, "Linea")).FirstOrDefault();
            op.Linea.Estado = EstadoLinea.LIBRE;
            await _repositorio.DeleteAsync(id);

            return Accepted(new { Id = id });
        }
        #endregion
    }
}
public record OrdenDto(int id,string nroOp, int modeloId, int colorId, int lineaId, string email);