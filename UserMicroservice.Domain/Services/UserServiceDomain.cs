using UserMicroservice.Domain.Repositories.Interface;
using UserMicroservice.Domain.ValueObjects;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Domain.Services
{
    public class UserServiceDomain : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Valida credenciais de login
        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.Login(username, password, DateTime.Now);
            if (user == null) return false;

            var passwordVO = new Password(password);
            return user.Authenticate(passwordVO.Value);
        }

        // Verifica se o username está disponível
        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            var user = await _userRepository.GetByUsername(username);

            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3)
                throw new ArgumentException("Username deve ter pelo menos 3 caracteres.");

            return user == null;
        }

        // registrar novo usuário
        public async Task<User> RegisterUserAsync(string username, string email, string password, string role)
        {
            if (!await IsUsernameAvailableAsync(username))
                throw new InvalidOperationException("Username já está em uso.");

            var emailVO = new Email(email);
            var passwordVO = new Password(password);

            var user = new User(new Guid(),username, emailVO.Address, passwordVO.Value, role);
            await _userRepository.Register(user);

            return user;
        }

        // redefinir senha
        public async Task ResetPasswordAsync(string username, string newPassword)
        {
            var user = await _userRepository.GetByUsername(username);
            if (user == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var passwordVO = new Password(newPassword);
            user.SetPassword(passwordVO.Value);

            await _userRepository.Update(user);
        }
    }
}