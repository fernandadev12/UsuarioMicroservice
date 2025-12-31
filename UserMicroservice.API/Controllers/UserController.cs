using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;

namespace UsuarioMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;
        private readonly IMediator _mdtr;
        private readonly IConfiguration _config;

        public UserController(IUserService usuarioService, IMediator mdtr, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _mdtr = mdtr;
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
        {
            var usuario = await _usuarioService.Login(loginDto.Username, loginDto.Password, DateTime.Now);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos");

            // Gerar claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Role.ToLower())
            };

            // Chave e credenciais
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                User = new UserDTO
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Role = usuario.Role
                }
            });
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
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