using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public UserDTO Data { get; set; }
        public UpdateUserCommand(UserDTO data)
        {
            Data = data;
        }
    }
}
