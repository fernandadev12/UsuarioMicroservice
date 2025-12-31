using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    // ATUALIZAR
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Data.Id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var updatedUserDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };


            var updatedUser = new User(
                request.Data.Id,
                request.Data.Username,
                request.Data.Email ?? user.Email,
                user.Password,
                request.Data.Role
            );

            await _repository.Update(updatedUser);

            return updatedUserDTO;
        }
    }
}