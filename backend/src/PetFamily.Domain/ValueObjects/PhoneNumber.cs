using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public const int MAX_LENGTH = 12;
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGTH)
            {
                return Result.Failure<PhoneNumber>("PhoneNumber is invalid");
            }

            return new PhoneNumber(value);
        }
    }
}
