using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
        public UserDTO UserDTO { get; set; }

        public UpdateUserCommand(UserDTO userDto)
        {
            Id = userDto.Id;
            UserDTO = userDto;
        }
    }
}