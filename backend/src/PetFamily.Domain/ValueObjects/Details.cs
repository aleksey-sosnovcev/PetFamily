using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public record Details
    {
        public string Name { get; }
        public string Description { get; }

        private Details(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<Details> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Details>("Name cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Details>("Description cannot be empty");
            }

            return new Details(name, description);
        }
    }
}
