using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Application.Handles
{
    // ATUALIZAR
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserService _service;
        public UpdateUserHandler(IUserService service)
        {
            _service = service;
        }


        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _service.GetById(request.Id);

            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            var updatedUserDTO = await BuildUpdatedUserDTO(request, user);

            await _service.Update(request.UserDTO);

            return updatedUserDTO;
        }

        private async Task<UserDTO> BuildUpdatedUserDTO(UpdateUserCommand request, User user)
        {
            var updatedUserDTO = new UserDTO
            {
                Id = user.Id,
                Username = request.UserDTO.Username ?? user.Username,
                Email = request.UserDTO.Email ?? user.Email.Address,
                Role = request.UserDTO.Role ?? user.Role,
                Password = request.UserDTO.Password != null
                    ? user.Password.Change(request.UserDTO.Password) 
                    : user.Password.Value,
                DataModificacao = DateTime.Now
            };

            return updatedUserDTO;
        }   

    }

}