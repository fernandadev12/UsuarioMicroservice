using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString() => Value;
        public override bool Equals(object? obj) =>
            obj is Username username && Value == username.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator string(Username v)
        {
            throw new NotImplementedException();
        }
    }
}
