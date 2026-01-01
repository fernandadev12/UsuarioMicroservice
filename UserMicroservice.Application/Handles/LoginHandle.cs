using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services.Interface;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    public class LoginHandle : IRequestHandler<LoginUserCommand, LoginUserDTO>
    {
        private readonly IUserService _service;
        public LoginHandle(IUserService service)
        {
            _service = service;
        }

        public async Task<LoginUserDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _service.Login(request.Data.Username, request.Data.Password, DateTime.Now);
            return await Task.FromResult(new LoginUserDTO
            {
                Username = request.Data.Username,
                Password = request.Data.Password
      
            });
        }
     


    } 

}
