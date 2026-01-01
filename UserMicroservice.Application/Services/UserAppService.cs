using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Services
{
    public class UserAppService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUserList()
        {
            return await _userRepository.GetAllUserList();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task Register(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username é obrigatório");

            var hashedPassword = user.Password.Length > 0 ? BCrypt.Net.BCrypt.HashPassword(user.Password) : null;
            var user_password = hashedPassword != null ? new UserMicroservice.Domain.ValueObjects.Password(hashedPassword) : null;

            var novoUsuario = new User(
                Guid.NewGuid(),
                user.Username,
                user.Email,
                user_password,
                user.Role);

            await _userRepository.Register(novoUsuario);
        }

        public async Task<User> Update(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username é obrigatório");

            var hashedPassword = user.Password.Length > 0 ? BCrypt.Net.BCrypt.HashPassword(user.Password) : null;
            var user_password = hashedPassword != null ? new UserMicroservice.Domain.ValueObjects.Password(hashedPassword) : null;

            var updateUsuario = new User(
                Guid.NewGuid(),
                user.Username,
                user.Email,
                user_password,
                user.Role);
            return await _userRepository.Update(updateUsuario);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<User> Login(string username, string password, DateTime dataAcesso)
        {
            var user = await _userRepository.Login(username, password, dataAcesso);

            if (user == null || !user.ComparePassword(password))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetByUsername(username);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return user;
        }

        public async Task<bool> SendEmailNewRegisterOrLogin(string email)
        {
            try
            {
                await _userRepository.SendEmailNewRegisterOrLogin(email);               
            }
            catch (Exception ex)
            {
                throw new Exception("Erro enviar email de notificação: " + ex.Message);
            }
            return true;
        }
    }
}