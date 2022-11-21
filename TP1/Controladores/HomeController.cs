using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace TP1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        private readonly IRepositorioGenerico<Color> _repositorio;

        public HomeController(IRepositorioGenerico<Color> repositorio)
        {
            this._repositorio = repositorio;
        }

        [HttpGet(Name = "GetColores")]
        public IActionResult GetColores()
        {
            return Ok();
        }
    }
}
