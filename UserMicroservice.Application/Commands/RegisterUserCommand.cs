using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Commands
{
    public class RegisterUserCommand : IRequest<UserDTO>
    {
        public RegisterUserDTO Data { get; set; }
        public RegisterUserCommand(RegisterUserDTO data)
        {
            Data = data;
        }
    }
}
