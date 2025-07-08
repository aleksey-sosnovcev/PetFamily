using CSharpFunctionalExtensions;

using PetFamily.Domain.Shared;


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


        public static Result<Details, Error> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsInvalid("Name");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            var details = new Details(name, description);

            return details;

        }
    }
}
