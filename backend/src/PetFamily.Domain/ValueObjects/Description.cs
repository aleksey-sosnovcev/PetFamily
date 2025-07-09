using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects
{
    public record Description
    {
        public const int MAX_LENGTH = 2000;
        public string Value { get; }

        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_DESCRIPTION_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            var description = new Description(value);

            return description;
        }
    }
}
