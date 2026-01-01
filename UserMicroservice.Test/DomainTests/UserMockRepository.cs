using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Test.DomainTests
{
    public class UserMockRepository : IUserRepository
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUserList()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(string username, string password, DateTime dataAcesso)
        {
            throw new NotImplementedException();
        }

        public Task Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailNewRegisterOrLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}