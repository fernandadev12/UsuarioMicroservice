using UserMicroservice.Domain.ValueObjects;

namespace UserMicroservice.Application.DTO
{
    public class RegisterUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public Email Email { get; set; } = new Email(string.Empty);
        public Password Password { get; set; } = new Password(string.Empty);
        public string Role { get; set; } = string.Empty;
    }
}
