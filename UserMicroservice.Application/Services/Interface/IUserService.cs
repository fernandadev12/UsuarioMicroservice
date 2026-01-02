using UserMicroservice.Application.DTO;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Application.Services.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllUserList();
        Task<User> GetById(Guid id);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        Task<bool> Register(UserDTO user);
        Task<User> Update(UserDTO user);
        Task<bool> Delete(Guid id);
        Task<User> Login(string username, string password, DateTime dataAcesso);
        Task <bool> SendEmailNewRegisterOrLogin(string email);
    }
}
