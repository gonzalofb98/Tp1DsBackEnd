using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorIncidencias : ControllerBase
    {
        private readonly IIncidenciaService _incidenciaService;

        public ControladorIncidencias(IIncidenciaService incidenciaService)
        {
            this._incidenciaService = incidenciaService;
        }


        #region Metodos Get
        [HttpGet("Incidencias")]
        public async Task<IActionResult> GetIncidencias()
        {
            return Ok(await _incidenciaService.GetTodosAsync());
        }

        [HttpGet("ByTipo")]
        public async Task<IActionResult> GetIncidenciaByTipo(int tipo)
        {
            return Ok((await _incidenciaService.GetConFiltro(x => x.Tipo == (TipoIncidencia) tipo)).ToList());
        }

        [HttpGet("ByDefecto")]
        public async Task<IActionResult> GetModelByDefecto(string defecto)
        {
            try
            {
                List<Incidencia> incidencias = await _incidenciaService.GetByDefecto(defecto);
                return Ok(incidencias);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Metodos Put, Post y Delete
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateModel(int id, [FromBody] IncidenciaDto incidenciaDto)
        //{
        //    try
        //    {
        //        var response = await _incidenciaService.ModificarIncidencia(id, incidenciaDto);
        //        return Accepted(new
        //        {
        //            Id = response
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("Create")]
        public async Task<IActionResult> SaveIncidencia(string nroOp, [FromBody] IncidenciaDto incidenciaDto)
        {
            try
            {
                var incidencia = await _incidenciaService.CrearIncidencia(nroOp, incidenciaDto);
                return Created("Incidencia agregada exitosamente", incidencia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> DeleteModelBySku(int id)
        //{
        //    try
        //    {
        //        await _incidenciaService.EliminarModelo(id);
        //        return Accepted(new { Id = id });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion
    }
}
