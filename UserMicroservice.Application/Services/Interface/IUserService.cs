using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Application.Services.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllUserList();
        Task<User> GetById(Guid id);
        Task Register(User user);
        Task<User> Update(User user);
        Task<bool> Delete(Guid id);
        Task<User> Login(string username, string password, DateTime dataAcesso);
    }
}
