using Dominio.Contratos;
using Dominio.Entidades;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace TP1.Controllers;
[ApiController]
[Route("[controller]")]
public class ControladorModelos : Controller
{
    private readonly IModelService _modelService;

    public ControladorModelos(IModelService modelService)
    {
        this._modelService = modelService;
    }


    #region Metodos Get
    [HttpGet("Models")]
    public async Task<IActionResult> GetModels()
    {
        return Ok(await _modelService.GetTodosAsync());
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetModelById(int id)
    {
        return Ok(await _modelService.GetAsync(id));
    }

    [HttpGet("BySku")]
    public async Task<IActionResult> GetModelBySku(int sku)
    {
        return Ok(await _modelService.GetConFiltro(x => x.Sku == sku));
    }
    #endregion

    #region Metodos Put, Post y Delete
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateModel(int id, [FromBody] ModeloDto modeloDto)
    {
        try
        {
            var response = await _modelService.ModificarModelo(id, modeloDto);
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
    public async Task<IActionResult> SaveModel([FromBody] ModeloDto modeloDto)
    {
        try
        {
            var modelo = await _modelService.CrearModelo(modeloDto);
            return Created("", modelo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteModelBySku(int id)
    {
        try
        {
            await _modelService.EliminarModelo(id);
            return Accepted(new { Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}
