using MediatR;

namespace UserMicroservice.Application.Commands
{
    public class SendEmailCommand : IRequest
    {
        public string Email { get; set; }
        public SendEmailCommand(string email)
        {
            Email = email;
        }
    }
}
