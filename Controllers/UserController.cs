using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;

namespace UsuarioMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;
        private readonly IMediator _mdtr;
       

        public UserController(IUserService usuarioService, IMediator mdtr)
        {
            _usuarioService = usuarioService;
            _mdtr = mdtr;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
                return Unauthorized();

            var usuario = await _usuarioService.Login(username, password, DateTime.Now);

            if (usuario == null)
                return NotFound();

            var usuarioDto = new UserDTO
            {
                Id = usuario.Id,
                Username = usuario.Username
            };

            return Ok(usuarioDto);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllUserList();
            return Ok(usuarios);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var command = new RegisterUserCommand(registerUserDTO);
            var result = await _mdtr.Send(command);
            return Ok(result);
        }

    }
}