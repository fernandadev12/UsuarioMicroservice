using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserDTO>
    {
        public string Username { get; set; }
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetUserByEmailQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
