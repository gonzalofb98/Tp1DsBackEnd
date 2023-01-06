using Datos;
using Dominio.Contratos;
using Dominio.Entidades;
using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using TP1.Configuraciones;

namespace TP1.Controladores
{
    public record AuthenticateRequest(string Name, string Password);


    [ApiController]
    [Route("api/[controller]")]
    public class ControladorAutenticacion : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        ITokenHandlerService _service;

        public ControladorAutenticacion(UserManager<Usuario> userManager, ITokenHandlerService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpGet]
        [Route("exception")]
        public async Task<IActionResult> ThrowException()
        {
            throw new Exception("tst");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("El correo electronico indicado ya existe!");
                }
                var isCreated = await _userManager.CreateAsync(new Usuario { 
                    Name = user.Name,
                    LastName = user.LastName, 
                    UserName = user.Email,
                    Rol = (Rol)user.Rol,
                    Email = user.Email },
                    user.Password);

                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }

            }
            else
            {
                return BadRequest("Se produjo algun error al registrar el usuario");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var pars = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        PasswordHash = existingUser.PasswordHash,
                        UserName = existingUser.UserName
                    };

                    var jwtToken = _service.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDTO()
                    {
                        Login = true,
                        Token = jwtToken
                    });

                }
                else
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                    });
                }

            }
            else
            {
                return BadRequest(new UserLoginResponseDTO()
                {
                    Login = false,
                    Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                });
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email));
        }
    }
}
