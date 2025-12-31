using MediatR;
using UserMicroservice.Application.Commands;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Application.Handles
{
    // DELETAR
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Delete(request.Id);
        }
    }
}
