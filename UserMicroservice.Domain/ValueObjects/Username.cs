namespace UserMicroservice.Domain.ValueObjects
{
    public class Username
    {
        public string Value { get; private set; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Username é obrigatório");
            Value = value;
        }
    }
}
