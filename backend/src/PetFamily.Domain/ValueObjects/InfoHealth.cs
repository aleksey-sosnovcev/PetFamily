using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects
{
    public record InfoHealth
    {
        public string VValue { get; }

        private InfoHealth(string value)
        {
            VValue = value;
        }

        public static Result<InfoHealth, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Errors.General.ValueIsInvalid("InfoHealth");
            }

            var infoHealth = new InfoHealth(value);

            return infoHealth;
        }
    }
}
