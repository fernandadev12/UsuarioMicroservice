namespace UserMicroservice.Domain.ValueObjects
{
    public class Password
    {
        public string Value { get; private set; }

        public Password(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length < 6)
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");

            Value = value;
        }

        public bool Compare(string password) => Value == password;

        public string Change(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
                throw new ArgumentException("Nova senha inválida. Mínimo 6 caracteres.");

            Value = newPassword;

            return Value;
        }
    }
}