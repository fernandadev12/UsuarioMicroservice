using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    // REGISTRAR
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IUserRepository _repository;

        public RegisterUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User(
                      Guid.NewGuid(),
                      request.Data.Username,
                      request.Data.Email,
                      request.Data.Password,
                      request.Data.Role
                  );

                await _repository.Register(user);

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar o usuário: " + ex.Message);
            }

        }
    }
}
