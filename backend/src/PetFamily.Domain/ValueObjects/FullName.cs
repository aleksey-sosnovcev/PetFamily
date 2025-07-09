using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects
{
    public record FullName
    {
        public string Surname { get; }
        public string FirstName { get; }
        public string Patronymic { get; }

        private FullName(string surname, string firstName, string patronymic)
        {
            Surname = surname;
            FirstName = firstName;
            Patronymic = patronymic;
        }

        public static Result<FullName, Error> Create(string surname, string firstName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                return Errors.General.ValueIsInvalid("Surname");
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Errors.General.ValueIsInvalid("FirstName");
            }
            if (string.IsNullOrWhiteSpace(patronymic))
            {
                return Errors.General.ValueIsInvalid("Patronymic");
            }

            var fullname = new FullName(surname, firstName, patronymic);

            return fullname;
        }
    }
}
