using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Domain.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserList();
        Task Register(User user);
        Task<User> Update(User user);
        Task<User> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<User> Login(string username, string password, DateTime dataAcesso);
        Task<User> GetByUsername(string username);
    }
}
