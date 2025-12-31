using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Commands
{
    public class LoginUserCommand : IRequest<UserDTO>
    {
        public UserDTO Data { get; set; }
        public LoginUserCommand(UserDTO data)
        {
            Data = data;
        }
    }
}
