using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Commands
{
    public class LoginUserCommand : IRequest<LoginUserDTO>
    {
        public LoginUserDTO Data { get; set; }
        public LoginUserCommand(LoginUserDTO data)
        {
            Data = data;
        }
    }
}
