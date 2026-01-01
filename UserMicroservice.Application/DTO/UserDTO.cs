using UserMicroservice.Domain.ValueObjects;

namespace UserMicroservice.Application.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Email? Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime DataModificacao { get; set; } = DateTime.Now;

    }
}
