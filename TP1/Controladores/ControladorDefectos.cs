using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControladorDefectos : Controller
    {
        private readonly IRepositorioGenerico<Defecto> _repositorio;

        public ControladorDefectos(IRepositorioGenerico<Defecto> repositorio)
        {
            this._repositorio = repositorio;
        }

        #region Metodos Get
        [HttpGet("Defects")]
        public async Task<IActionResult> GetDefects()
        {
            return Ok(await _repositorio.GetTodosAsync());
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetDefectById(int id)
        {
            return Ok(await _repositorio.GetAsync(id));
        }

        [HttpGet("ByDescription")]
        public async Task<IActionResult> GetDefectByDescription(string description)
        {
            return Ok(await _repositorio.GetConFiltro(x => x.Descripcion == description));
        }
        #endregion

        #region Metodos Put, Post y Delete
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDefect(int id, [FromBody] DefectDto defectDto)
        {
            if (defectDto == null)
            {
                return BadRequest();
            }
            else
            {
                //Cambiar por servicios con mapper
                var defect = new Defecto(
                    defectDto.descripcion,
                    (TipoDefecto) defectDto.tipo);
                defect.Id = id;
                var response = await _repositorio.UpdateAsync(defect);
                return Accepted(new
                {
                    Id = response
                });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> SaveDefect([FromBody] DefectDto defectDto)
        {
            try
            {
                if (defectDto.descripcion == "") return BadRequest("No posee una descripcion");
                var defectoExistente = (await _repositorio.ListAsync(x => x.Descripcion == defectDto.descripcion)).FirstOrDefault();
                if (defectoExistente != null)
                {
                    return BadRequest("El Defecto ya existe");
                }
                else
                {
                    var defect = new Defecto(
                        defectDto.descripcion,
                        (TipoDefecto)defectDto.tipo);
                    await _repositorio.AgregarAsync(defect);
                    return Created("", defect);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteDefect(int id)
        {
            await _repositorio.DeleteAsync(id);

            return Accepted(new { Id = id });
        }
        #endregion
    }
}
public record DefectDto(int id, string descripcion, int tipo);