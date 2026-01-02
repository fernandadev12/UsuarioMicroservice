using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;

namespace UserMicroservice.Application.Handles
{
    // REGISTRAR
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IUserService _service;

        public RegisterUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
{
                var user = new UserDTO();
                user.Id = Guid.NewGuid();
                user.Username = request.Data.Username;
                user.Email = request.Data.Email;
                user.Password = request.Data.Password;
                user.Role = request.Data.Role;
                 

                await _service.Register(user);

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
