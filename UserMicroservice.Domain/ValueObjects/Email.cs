namespace UserMicroservice.Domain.ValueObjects
{
    public class Email
    {

        public string Address { get; set; }

        public Email(string address)
        {
            if (!String.IsNullOrEmpty(address) && !address.Contains("@"))
                throw new ArgumentException("Email inválido.");

            Address = address;
        }

        public override string ToString() => Address;
    }

}
