using MediatR;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Queries;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    public class GetAllUsersListQueryHandler : IRequestHandler<GetAllUsersListQuerie, List<UserDTO>>
    {
        private readonly IUserRepository _repository;

        public GetAllUsersListQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersListQuerie request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUserList();
            var userDTOs = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email.Address,
                Role = user.Role
            }).ToList();
            
            return userDTOs;
        }
    }

}
