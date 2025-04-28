using CSharpFunctionalExtensions;

namespace PetFamily.Domain
{
    public record Description
    {
        public const int MAX_LENGTH = 2000;
        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGTH)
            {
                return Result.Failure<Description>("Description is invalid");
            }

            return new Description(value);
        }
    }
}
