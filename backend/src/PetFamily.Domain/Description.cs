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

        public static Result<Description, Error> Create(string value)
        {
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Description.cs
            if (string.IsNullOrWhiteSpace(value) || value.Length < MAX_LENGTH)
=======
            if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_DESCRIPTION_LENGTH)
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/ValueObjects/Description.cs
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            var description = new Description(value);

            return description;
        }
    }
}
