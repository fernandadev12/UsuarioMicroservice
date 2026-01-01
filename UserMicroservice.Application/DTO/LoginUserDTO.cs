namespace UserMicroservice.Application.DTO
{
    public class LoginUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DataAcesso { get; set; } = DateTime.UtcNow.ToString();
    }
}
