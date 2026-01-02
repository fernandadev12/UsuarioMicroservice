using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Domain.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserList();
        Task<bool> Register(User user);
        Task<User> Update(User user);
        Task<User> GetByEmail(string email);
        Task<bool> Delete(Guid id);
        Task<User> Login(string username, string password, DateTime dataAcesso);
        Task<User> GetByUsername(string username);
        Task<User> GetById(Guid id);
        Task<bool> SendEmailNewRegisterOrLogin(string email);
    }
}
