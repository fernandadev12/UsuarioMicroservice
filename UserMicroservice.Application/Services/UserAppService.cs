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

        public async Task Register(User user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new Exception("Username é obrigatório");

            await _userRepository.Register(user);
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
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
    }
}