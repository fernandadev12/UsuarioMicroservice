using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Queries;

namespace UsuarioMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mdtr;
        private readonly IConfiguration _config;

        public UserController(IMediator mdtr, IConfiguration config)
        {
            _mdtr = mdtr;
            _config = config;
        }


        /// <summary>
        /// Autentica o usuário e gera o token JWT
        /// </summary>
        /// <param name="loginDto">username e password</param>
        /// <returns>bearer token</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
        {
            if (loginDto.Username == null || loginDto.Password == null)
                return Unauthorized("Usuário ou senha inválidos");

            var login = new LoginUserCommand(loginDto);
            var retorno = _mdtr.Send(login);

            // Gerar claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, retorno.Id.ToString()),
                new Claim(ClaimTypes.Name, retorno.Result.Username)
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
                loginUser = new LoginUserDTO
                {
                    Username = retorno.Result.Username,
                    DataAcesso = DateTime.Now.ToString()
                }
            });
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados acesso somente admin
        /// </summary>
        /// <returns>Todos usuários cadastrados</returns>
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var listUsers = new GetAllUsersListQuerie();
            var result = await _mdtr.Send(listUsers);
            return Ok(result);
        }

        /// <summary>
        /// Retorna o usuário pelo username acesso somente admin
        /// </summary>
        /// <param name="username">username do usuario</param>
        /// <returns>usuario cadastrado</returns>
        [HttpGet("GetUserByUsername/{username}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var getUserByUsername = new GetUserByUsernameQuery(username);
            var result = await _mdtr.Send(getUserByUsername);
            return Ok(result);
        }

        /// <summary>
        /// Retorna o usuário pelo email acesso somente admin
        /// </summary>
        /// <param name="email">email do usuario</param>
        /// <returns>usuario cadastrado</returns>
        [HttpGet("GetUserByEmail/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var getUserByEmail = new GetUserByEmailQuery(email);
            var result = await _mdtr.Send(getUserByEmail);
            return Ok(result);
        }

        /// <summary>
        /// Deleta o usuário pelo id , acesso somente admin
        /// </summary>
        /// <param name="id">Id do usuario a ser deletado</param>
        /// <returns>true para item deletado</returns>
        [HttpGet("DeleteUser/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var usuarioRemover = new DeleteUserCommand(id);
            var result = await _mdtr.Send(usuarioRemover);
            return Ok(result);
        }

        /// <summary>
        /// Atualiza o usuário pelo id , acesso somente admin
        /// </summary>
        /// <param name="id">id do usuario</param>
        /// <returns>true se atualizado com sucesso</returns>
        [HttpGet("UpdateUser/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            var updateUsuario = new UpdateUserCommand(user);
            var result = await _mdtr.Send(updateUsuario);

            return Ok(result);
        }

        /// <summary>
        /// Envia um email para o novo cadastrado ou login
        /// </summary>
        /// <param name="email">email a ser enviado com informação</param>
        /// <returns>true se enviado com sucesso</returns>
        [HttpGet("SendEmailNewRegisterOrLogin/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SendEmailNewRegisterOrLogin(string email)
        {
            var emailUsuario = new SendEmailCommand(email);
            var result = _mdtr.Send(emailUsuario);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo usuário acesso admin e usuario 
        /// </summary>
        /// <param name="registerUserDTO">Dados do usuário</param>
        /// <returns>objeto cadastrado com sucesso</returns>
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