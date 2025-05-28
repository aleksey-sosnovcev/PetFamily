using CSharpFunctionalExtensions;

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

        public static Result<FullName> Create(string surname, string firstName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                return Result.Failure<FullName>("Surname cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<FullName>("FirstName cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(patronymic))
            {
                return Result.Failure<FullName>("Patronymic cannot be empty");
            }

            return new FullName(surname, firstName, patronymic);
        }
    }
}
