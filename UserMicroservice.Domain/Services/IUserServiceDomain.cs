using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Domain.Services
{
    public interface IUserDomainService
    {
        // Valida credenciais de login
        Task<bool> ValidateUserCredentialsAsync(string username, string password);

        // Verifica se o username está disponível
        Task<bool> IsUsernameAvailableAsync(string username);

        // Registrar novo usuário
        Task<User> RegisterUserAsync(string username, string email, string password, string role);

        //  redefinir senha
        Task ResetPasswordAsync(string username, string newPassword);
    }
}