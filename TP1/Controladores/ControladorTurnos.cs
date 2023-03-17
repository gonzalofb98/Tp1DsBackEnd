using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Herramientas;
using Services.Interfaces;
using System.Drawing;
using System.Globalization;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorTurnos : ControllerBase
    {
        private readonly ITurnService _turnService;

        public ControladorTurnos(ITurnService turnService)
        {
            this._turnService = turnService;
        }

        #region Metodos Get
        [HttpGet("Turns")]
        public async Task<IActionResult> GetTurns()
        {
            return Ok(await _turnService.GetTodosAsync());
        }

        [HttpGet("CurrentTurn")]
        public async Task<IActionResult> GetTurnoActual()
        {
            var turnoActual = Utils.GetTurnoActual((await _turnService.GetTodosAsync()).ToList()) ;
            return Ok(turnoActual);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetTurnById(int id)
        {
            return Ok(await _turnService.GetAsync(id));
        }

        [HttpGet("ByDescripcion")]
        public async Task<IActionResult> GetTurnByDescripcion(string desc)
        {
            return Ok((await _turnService.ListAsync(x => x.Descripcion == desc)).FirstOrDefault());
        }

        [HttpGet("TotalHorasByDescripcion")]
        public async Task<IActionResult> GetTotalHorasByDescripcion(string desc)
        {
            try
            {
                var listaHoras = await _turnService.GetTotalHorasByDescripcion(desc);
                return Ok(listaHoras);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTurn(int id, [FromBody] TurnoDto turnDto)
        {
            try
            {
                var response = _turnService.ModificarTurno(id, turnDto);
                return Ok(new
                {
                    Id = response
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveTurn([FromBody] TurnoDto turnDto)
        {
            try
            {
                var response = await _turnService.CrearTurno(turnDto);
                return Created("", response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteLine(int id)
        {
            try
            {
                await _turnService.EliminarTurno(id);
                return Accepted(new { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
