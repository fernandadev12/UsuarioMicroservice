using MediatR;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Queries;
using UserMicroservice.Application.Services.Interface;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    public class GetUserByUsernameHandle : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        private readonly IUserService _service;
        public GetUserByUsernameHandle(IUserService service)
        {
            _service = service;
        }
        
        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var getUser = await _service.GetUserByUsername(request.Username);
            return await Task.FromResult(new UserDTO
            {
                Id = getUser.Id,
                Username = getUser.Username,
                Email = getUser.Email,
                Role = getUser.Role
            });
        }
    }

    public class GetUserByEmailHandle : IRequestHandler<GetUserByEmailQuery, UserDTO>
    {
        private readonly IUserService _service;
        public GetUserByEmailHandle(IUserService service)
        {
            _service = service;
        }

        public Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var getUser = _service.GetUserByEmail(request.Email);
            return Task.FromResult(new UserDTO
            {
                Id = getUser.Result.Id,
                Username = getUser.Result.Username,
                Email = getUser.Result.Email,
                Role = getUser.Result.Role
            });
        }
    }
}
