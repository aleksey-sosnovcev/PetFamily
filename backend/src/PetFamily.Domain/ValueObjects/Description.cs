using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects
{
    public record Description
    {
        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < Constants.MAX_DESCRIPTION_LENGTH)
            {
                return Result.Failure<Description>("Description is invalid");
            }

            return new Description(value);
        }
    }
}
