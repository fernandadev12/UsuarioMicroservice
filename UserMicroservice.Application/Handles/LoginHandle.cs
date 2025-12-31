using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    public class LoginHandle : IRequestHandler<LoginUserCommand, UserDTO>
    {
        private readonly IUserRepository _repository;
        public LoginHandle(IUserRepository repository)
        {
            _repository = repository;
        }
        public Task<UserDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var usuario = _repository.Login(request.Data.Username, request.Data.Password, DateTime.Now);
            return Task.FromResult(new UserDTO
            {
                Username = request.Data.Username,
                Password = request.Data.Password,
                Role = usuario.Result.Role
               
             });
        }
    } 

}
