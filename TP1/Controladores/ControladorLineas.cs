using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorLineas : ControllerBase
    {
         private readonly IRepositorioGenerico<LineaDeTrabajo> _repositorio;

    public ControladorLineas(IRepositorioGenerico<LineaDeTrabajo> repositorio)
    {
        this._repositorio = repositorio;
    }

    #region Metodos Get
    [HttpGet("Lines")]
    public async Task<IActionResult> GetLines()
    {
        return Ok(await _repositorio.GetTodosAsync());
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetLineById(int number)
    {
        return Ok(await _repositorio.GetAsync(number));
    }
    #endregion

    #region Metodos Put, Post y Delete
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLine(int id, [FromBody] LineaDto lineaDto)
    {
        if (lineaDto == null) { 
            return BadRequest(); 
        }
        else 
        {
            //Cambiar por servicios con mapper
            var line = new LineaDeTrabajo(lineaDto.numero);
                line.Id = id;
                line.Estado = lineaDto.estado;
            var response = await _repositorio.UpdateAsync(line);
            return Accepted(new
            {
                Id = response
            });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> SaveLine([FromBody] LineaDto lineaDto)
    {
        if (lineaDto.numero == 0) return BadRequest("No puede haber una linea 0");
        if (await _repositorio.GetAsync(lineaDto.numero) != null)
        {
            return BadRequest("La linea que intenta crear ya existe");
        }
        else
        {
            //Cambiar por servicios con mapper
            var line = new LineaDeTrabajo(lineaDto.numero);
            await _repositorio.AgregarAsync(line);
            return Created("", line);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteLine(int id)
    {
        await _repositorio.DeleteAsync(id);

        return Accepted( new { Id = id } );
    }
    #endregion
    }
}
public record LineaDto(int id, int numero, EstadoLinea estado);