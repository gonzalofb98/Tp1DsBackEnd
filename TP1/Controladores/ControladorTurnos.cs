using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Herramientas;
using System.Globalization;

namespace TP1.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorTurnos : ControllerBase
    {
        private readonly IRepositorioGenerico<Turno> _repositorio;

        public ControladorTurnos(IRepositorioGenerico<Turno> repositorio)
        {
            this._repositorio = repositorio;
        }

        #region Metodos Get
        [HttpGet("Turns")]
        public async Task<IActionResult> GetTurns()
        {
            return Ok(await _repositorio.GetTodosAsync());
        }

        [HttpGet("CurrentTurn")]
        public async Task<IActionResult> GetTurnoActual()
        {
            var turnoActual = Utils.GetTurnoActual((await _repositorio.GetTodosAsync()).ToList()) ;
            return Ok(turnoActual);
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetTurnById(int id)
        {
            return Ok(await _repositorio.GetAsync(id));
        }

        [HttpGet("ByDescripcion")]
        public async Task<IActionResult> GetTurnByDescripcion(string desc)
        {
            return Ok((await _repositorio.ListAsync(x => x.Descripcion == desc)).FirstOrDefault());
        }

        [HttpGet("TotalHorasByDescripcion")]
        public async Task<IActionResult> GetTotalHorasByDescripcion(string desc)
        {
            if(desc == null ||desc == "") return BadRequest("Error, ingrese una descripcion ");
            var turno = (await _repositorio.ListAsync(x => x.Descripcion == desc)).FirstOrDefault();
            if(turno == null) return BadRequest("Error, el turno no existe");
            var listaHoras = turno.getTotalHoras();
            return Ok(listaHoras);
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTurn(int id, [FromBody] TurnoDto turnDto)
        {
            if (turnDto == null)
            {
                return BadRequest();
            }
            else
            {
                DateTime horaInicio;
                DateTime horaFin;

                if (DateTime.TryParseExact(turnDto.horaInicio, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaInicio) && 
                    DateTime.TryParseExact(turnDto.horaFin, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaFin))
                {
                    var newTurno = new Turno(horaInicio, horaFin, turnDto.descripcion);
                    newTurno.Id = id;
                    var response = await _repositorio.UpdateAsync(newTurno);
                    return Accepted(new
                    {
                        Id = response
                    });
                }
                else
                {
                    return BadRequest();
                }
                
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveTurn([FromBody] TurnoDto turnDto)
        {
            if (turnDto == null) return BadRequest("Se ingreso incorrectamente los datos del turno");
            if ((await _repositorio.ListAsync(x => x.Descripcion == turnDto.descripcion)).FirstOrDefault() != null)
            {
                return BadRequest("El turno que intenta crear ya existe");
            }
            else
            {
                DateTime horaInicio;
                DateTime horaFin;

                if (DateTime.TryParseExact(turnDto.horaInicio, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaInicio) &&
                    DateTime.TryParseExact(turnDto.horaFin, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out horaFin))
                {
                    var newTurno = new Turno(horaInicio, horaFin, turnDto.descripcion);
                    var response = await _repositorio.AgregarAsync(newTurno);
                    return Created("", newTurno);
                }
                else
                {
                    return BadRequest();
                }
                //Cambiar por servicios con mapper
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteLine(int id)
        {
            await _repositorio.DeleteAsync(id);

            return Accepted(new { Id = id });
        }
        #endregion
    }
}
public record TurnoDto(string horaInicio, string horaFin, string descripcion);