using CSharpFunctionalExtensions;

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

        public static Result<Name> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGTH)
            {
                return Result.Failure<Name>("Name cannot be empty");
            }

            return new Name(value);
        }
    }
}
