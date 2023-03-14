using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using System.Drawing;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorDefectos : Controller
    {
        private readonly IDefectService _defectService;

        public ControladorDefectos(IDefectService defectService)
        {
            this._defectService = defectService;
        }

        #region Metodos Get
        [HttpGet("Defects")]
        public async Task<IActionResult> GetDefects()
        {
            return Ok(await _defectService.GetTodosAsync());
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetDefectById(int id)
        {
            return Ok(await _defectService.GetAsync(id));
        }

        [HttpGet("ByDescription")]
        public async Task<IActionResult> GetDefectByDescription(string description)
        {
            return Ok(await _defectService.GetConFiltro(x => x.Descripcion == description));
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDefect(int id, [FromBody] DefectoDto defectDto)
        {
            try
            {
                var response = await _defectService.ModificarDefecto(id, defectDto);
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
        public async Task<IActionResult> SaveDefect([FromBody] DefectoDto defectDto)
        {
            try
            {
                var color = await _defectService.CrearDefecto(defectDto);
                return Created("", color);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteDefect(int id)
        {
            try
            {
                await _defectService.EliminarDefecto(id);
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
