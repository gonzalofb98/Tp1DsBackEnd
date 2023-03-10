﻿using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Datos.Migrations;
using System.Collections.Generic;
using Services.Herramientas;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorOrdenDeProduccion : ControllerBase
    {
        private readonly IRepositorioGenerico<OrdenDeProduccion> _repositorio;
        private readonly IRepositorioGenerico<Modelo> _repositorioModelo;
        private readonly IRepositorioGenerico<Color> _repositorioColor;
        private readonly IRepositorioGenerico<Turno> _repositorioTurno;
        private readonly IRepositorioGenerico<LineaDeTrabajo> _repositorioLinea;
        private readonly UserManager<Usuario> _userManager;

        public ControladorOrdenDeProduccion(
            IRepositorioGenerico<OrdenDeProduccion> repositorio,
            IRepositorioGenerico<Modelo> repositorioModelo,
            IRepositorioGenerico<Color> repositorioColor,
            IRepositorioGenerico<Turno> repositorioTurno,
            IRepositorioGenerico<LineaDeTrabajo> repositorioLinea,
            UserManager<Usuario> userManager
            )
        {
            this._repositorio = repositorio;
            this._repositorioLinea= repositorioLinea;
            this._repositorioModelo = repositorioModelo;
            this._repositorioColor = repositorioColor;
            this._repositorioTurno = repositorioTurno;
            this._userManager = userManager;
        }


        #region Metodos Get
        [HttpGet("OrdenesDeProduccion")]
        public async Task<IActionResult> GetOps()
        {
            var ops = await _repositorio.ListAsync("Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad");
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
            try
            {
                var op = (await _repositorio
                    .ListAsync(x => x.SupervisorDeLinea.Email == email, 
                    "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .Where(e => e.Estado != EstadoOp.FINALIZADA).FirstOrDefault();
                if (op != null)
                    return Ok(op);
                else return Ok("Usuario no encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("ByUsuarioCalidad")]
        public async Task<IActionResult> GetOpByUsuarioCalidad(string email)
        {
            try
            {
                var op = (await _repositorio
                    .ListAsync(x => x.SupervisorDeCalidad.Email == email,
                    "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .Where(e => e.Estado != EstadoOp.FINALIZADA).FirstOrDefault();
                if (op != null)
                    return Ok(op);
                else return Ok("Usuario no encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("ByLinea")]
        public async Task<IActionResult> GetOpByLinea(int lineaId)
        {
            try
            {
                var op = (await _repositorio
                    .ListAsync(x => x.Linea.Id == lineaId, 
                    "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .Where(e => e.Estado != EstadoOp.FINALIZADA).FirstOrDefault();
                return Ok(op);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("ChangeEstado")]
        public async Task<IActionResult> ChangeEstadoOp(string email)
        {
            var opExistente = (await _repositorio
                .ListAsync(x => x.SupervisorDeLinea.Email == email, 
                "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .Where(e => e.Estado != EstadoOp.FINALIZADA).FirstOrDefault();
                opExistente.PausarReanudarOrden();

            await _repositorio.UpdateAsync(opExistente);

            return Ok(opExistente);
        }

        [HttpPut("VoD")]
        public async Task<IActionResult> LinkOrUnlinkSupCalidad(string email, string numeroOp)
        {
            var supervisorDeCalidad = await _userManager.FindByEmailAsync(email);
            if (supervisorDeCalidad == null) return BadRequest("Error, el usuario no se encontró");
            var opExistente = (await _repositorio
                .ListAsync(x => x.Numero == numeroOp,
                "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .FirstOrDefault();
            if (opExistente == null) return BadRequest("Error, el usuario no se encontró");

            if(opExistente.SupervisorDeCalidad != null )
            {
                if (opExistente.SupervisorDeCalidad.Email == email) opExistente.SupervisorDeCalidad = null;
                else return BadRequest("Hay otro supervisor asignado a la Op");
            }
            else
            {
                opExistente.SupervisorDeCalidad = supervisorDeCalidad;
            }

            await _repositorio.UpdateAsync(opExistente);

            return Ok(opExistente);
        }

        [HttpPut("FinishOp")]
        public async Task<IActionResult> FinishOp(string email)
        {
            var opExistente = (await _repositorio
                .ListAsync(x => ((x.Estado != EstadoOp.FINALIZADA) && (x.SupervisorDeLinea.Email == email)), 
                "Modelo", "Color", "Linea", "SupervisorDeLinea", "SupervisorDeCalidad"))
                .FirstOrDefault();
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
            var opExistente = (await _repositorio.ListAsync(x => x.Numero == opDto.nroOp)).FirstOrDefault();
            if (opExistente != null)
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

                    var newOp = new OrdenDeProduccion(opDto.nroOp, modelo, color, linea, usuario);

                    var turnoActual = Utils.GetTurnoActual((await _repositorioTurno.GetTodosAsync()).ToList());

                    newOp.EstablecerNuevaJornada(DateTime.Now, turnoActual);

                    await _repositorio.AgregarAsync(newOp);

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