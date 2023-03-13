using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorLineas : ControllerBase
    {
        private readonly ILineService _LineService;

    public ControladorLineas(ILineService lineService)
    {
        this._LineService = lineService;
    }

    #region Metodos Get
    [HttpGet("Lines")]
    public async Task<IActionResult> GetLines()
    {
        return Ok(await _LineService.GetTodosAsync());
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetLineById(int number)
    {
        return Ok(await _LineService.GetAsync(number));
    }
    #endregion

    #region Metodos Put, Post y Delete
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLine(int id, [FromBody] LineaDeTrabajoDto lineaDto)
    {
            try
            {
                var response = await _LineService.ModificarLineaDeTrabajo(id, lineaDto);
                return Accepted(new
                {
                    Id = response
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> SaveLine([FromBody] LineaDeTrabajoDto lineaDto)
    {
            try
            {
                var line = await _LineService.CrearLineaDeTrabajo(lineaDto);
                return Created("", line);
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
                await _LineService.EliminarLineaDeTrabajo(id);
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