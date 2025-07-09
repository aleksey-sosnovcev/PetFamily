using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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

        public static Result<PhoneNumber, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Phone number");
            }

            var phoneNumber = new PhoneNumber(value);

            return phoneNumber;
        }
    }
}
