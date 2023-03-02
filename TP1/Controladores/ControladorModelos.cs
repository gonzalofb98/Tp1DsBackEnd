using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace TP1.Controllers;
[ApiController]
[Route("[controller]")]
public class ControladorModelos : Controller
{
    private readonly IRepositorioGenerico<Modelo> _repositorio;

    public ControladorModelos(IRepositorioGenerico<Modelo> repositorio)
    {
        this._repositorio = repositorio;
    }


    #region Metodos Get
    [HttpGet("Models")]
    public async Task<IActionResult> GetModels()
    {
        return Ok(await _repositorio.GetTodosAsync());
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetModelById(int id)
    {
        return Ok(await _repositorio.GetAsync(id));
    }

    [HttpGet("BySku")]
    public async Task<IActionResult> GetModelBySku(int sku)
    {
        return Ok(await _repositorio.GetConFiltro(x => x.Sku == sku));
    }
    #endregion

    #region Metodos Put, Post y Delete
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateModel(int id, [FromBody] ModeloDto modelo)
    {
        if (modelo == null) { 
            return BadRequest(); 
        }
        else 
        {
            //Cambiar por servicios con mapper
            var model = new Modelo(modelo.sku,
                modelo.descripcion,
                modelo.limiteInferiorReproceso,
                modelo.limiteSuperiorReproceso,
                modelo.limiteInferiorObservado,
                modelo.limiteSuperiorObservado);
            model.Id = id;
            var response = await _repositorio.UpdateAsync(model);
            return Accepted(new
            {
                Id = response
            });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> SaveModel([FromBody] ModeloDto modelo)
    {
        if (modelo.sku == 0) return BadRequest("Sku no puede ser 0");
        var modeloExistente = (await _repositorio.ListAsync(x => x.Sku == modelo.sku)).FirstOrDefault();
        if (modeloExistente != null)
        {
            return BadRequest("El Modelo ya existe");
        }
        else
        {
            //Cambiar por servicios con mapper
            var model = new Modelo(modelo.sku, 
                modelo.descripcion,
                modelo.limiteInferiorReproceso, 
                modelo.limiteSuperiorReproceso, 
                modelo.limiteInferiorObservado, 
                modelo.limiteSuperiorObservado);
            await _repositorio.AgregarAsync(model);
            return Created("", model);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteModelBySku(int id)
    {
        await _repositorio.DeleteAsync(id);

        return Accepted( new { Id = id } );
    }
    #endregion
}

public record ModeloDto(int id, int sku, string descripcion, int limiteInferiorReproceso,
        int limiteSuperiorReproceso, int limiteInferiorObservado, int limiteSuperiorObservado);
