using UserMicroservice.Domain.ValueObjects;

namespace UserMicroservice.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public Email? Email { get; private set; } 
        public Password Password { get; private set; } = new Password(string.Empty);
        public string Role { get; private set; } = string.Empty;
        public DateTime LastAccess { get; set; } = DateTime.Now;

        public User() { }

        // Construtor principal
        public User(Guid id, string username, string email, string password, string role)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username é obrigatório.", nameof(username));

            if (string.IsNullOrEmpty(role))
                throw new ArgumentException("Role é obrigatória.", nameof(role));

            Id = id; // atribuir o Id recebido
            Username = username;
            Email.Address = email;
            Password.Value = password;
            Role = role;
        }


        // Autenticação
        public bool Authenticate(string password)
        {
            return Password.Compare(password);
        }

        // Alterar senha
        internal void SetPassword(string newPassword)
        {
            Password.Change(newPassword);
        }

        // Alterar role
        internal void SetRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new ArgumentException("Role não pode ser vazia.");

            if (role != "Administrador" && role != "Usuario")
                throw new InvalidOperationException("Role inválida.");

            Role = role;
        }

        public bool ComparePassword(string password)
        {
            return Password.Value == password;

        }
    }
}