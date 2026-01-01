using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Application.Services.Interface;

namespace UserMicroservice.Application.Handles
{
    public class SendEmailHandle : IRequestHandler<SendEmailCommand>
    {
        private readonly IUserService _service;
        public SendEmailHandle(IUserService service)
        {
            _service = service;
        }
        public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            return _service.SendEmailNewRegisterOrLogin(request.Email);
        }
    }
}
