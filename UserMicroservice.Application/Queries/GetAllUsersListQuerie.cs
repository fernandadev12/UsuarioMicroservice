using MediatR;
using UserMicroservice.Application.DTO;

namespace UserMicroservice.Application.Queries
{
    public class GetAllUsersListQuerie : IRequest<List<UserDTO>>
    {
    }
}
