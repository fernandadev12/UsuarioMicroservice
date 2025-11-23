using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services;
using UserMicroservice.Application.Services.Interface;

namespace UsuarioMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;
        private UserAppService @object;

        public UserController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public UserController(UserAppService @object)
        {
            this.@object = @object;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
                return Unauthorized();

            var usuario = await _usuarioService.GetUserById(id);

            if (usuario == null)
                return NotFound();

            var usuarioDto = new UserDTO
            {
                Username = usuario.Username,
                Password = usuario.Password
            };

            return Ok(usuarioDto);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsers();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno ao listar usuários", details = ex.Message });
            }
        }

        public object Register(RegisterUserDTO registerUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}