using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP1.Controllers;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorColores : ControllerBase
    {
        private readonly IRepositorioGenerico<Color> _repositorio;

        public ControladorColores(IRepositorioGenerico<Color> repositorio)
        {
            this._repositorio = repositorio;
        }


        #region Metodos Get
        [HttpGet("Colors")]
        public async Task<IActionResult> GetColors()
        {
            return Ok(await _repositorio.GetTodosAsync());
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetColorById(int id)
        {
            return Ok(await _repositorio.GetAsync(id));
        }

        [HttpGet("ByCodigo")]
        public async Task<IActionResult> GetColorByCodigo(int codigo)
        {
            return Ok(await _repositorio.GetConFiltro(x => x.Codigo == codigo));
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] ColorDto color)
        {
            if (color == null)
            {
                return BadRequest();
            }
            else
            {
                //Cambiar por servicios con mapper
                var newColor = new Color(color.codigo,
                    color.descripcion);
                newColor.Id = id;
                var response = await _repositorio.UpdateAsync(newColor);
                return Accepted(new
                {
                    Id = response
                });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveColor([FromBody] ColorDto color)
        {
            if (color.codigo == 0) return BadRequest("Codigo no puede ser 0");
            if (await _repositorio.GetAsync(color.codigo) != null)
            {
                return BadRequest("El Codigo ya existe");
            }
            else
            {
                //Cambiar por servicios con mapper
                var newColor = new Color(color.codigo,
                    color.descripcion);
                await _repositorio.AgregarAsync(newColor);
                return Created("", newColor);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteColorByCodigo(int id)
        {
            await _repositorio.DeleteAsync(id);

            return Accepted(new { Id = id });
        }
        #endregion
    }
}

public record ColorDto(int id, int codigo, string descripcion);