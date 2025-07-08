using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain
{
    public record Name
    {
        public const int MAX_LENGTH = 20;
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Name");
            }

            var name = new Name(value);

            return name;
        }
    }
}
