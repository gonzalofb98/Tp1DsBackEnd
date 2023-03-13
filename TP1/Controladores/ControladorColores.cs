using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using TP1.Controllers;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorColores : ControllerBase
    {
        private readonly IColorService _colorService;

        public ControladorColores(IColorService repositorio)
        {
            this._colorService = repositorio;
        }


        #region Metodos Get
        [HttpGet("Colors")]
        public async Task<IActionResult> GetColors()
        {
            return Ok(await _colorService.GetTodosAsync());
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetColorById(int id)
        {
            return Ok(await _colorService.GetAsync(id));
        }

        [HttpGet("ByCodigo")]
        public async Task<IActionResult> GetColorByCodigo(int codigo)
        {
            return Ok(await _colorService.GetConFiltro(x => x.Codigo == codigo));
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] ColorDto colorDto)
        {
            try
            {
                var response = await _colorService.ModificarColor(id, colorDto);
                return Accepted(new
                {
                    Id = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveColor([FromBody] ColorDto colorDto)
        {
            try
            {
                var color = await _colorService.CrearColor(colorDto);
                return Created("", color);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteColorByCodigo(int id)
        {
            try
            {
                await _colorService.EliminarColor(id);
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