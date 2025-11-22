using UsersAPI.Models;

namespace UsersAPI.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(Guid id);
    }
}
