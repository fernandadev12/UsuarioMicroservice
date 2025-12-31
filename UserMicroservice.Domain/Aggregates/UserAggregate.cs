using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.ValueObjects;

namespace UserMicroservice.Domain.Aggregates
{
    public class UserAggregate
    {
        public User User { get; private set; }
        public List<UserPermission> Permissions { get; private set; } = new();

        public UserAggregate(string username, Email email, Password password, string role)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username é obrigatório.");

            User = new User(new Guid(),username, email, password, role);
            User.SetPassword(password.Value);
        }

        public bool Authenticate(string password)
        {
            return User.ComparePassword(password);
        }

        public void ChangeRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new ArgumentException("Role não pode ser vazia.");

            User.SetRole(role);
        }

        public void ResetPassword(Password newPassword)
        {
            User.SetPassword(newPassword.Value);
        }

        public void AddPermission(UserPermission permission)
        {
            if (Permissions.Exists(p => p.Name == permission.Name))
                throw new InvalidOperationException("Permissão já atribuída.");

            Permissions.Add(permission);
        }

        public void RemovePermission(string permissionName)
        {
            var permission = Permissions.Find(p => p.Name == permissionName);
            if (permission == null)
                throw new InvalidOperationException("Permissão não encontrada.");

            Permissions.Remove(permission);
        }
    }
}