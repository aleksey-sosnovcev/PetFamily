using CSharpFunctionalExtensions;

namespace PetFamily.Domain
{
    public record Details
    {
        public string Name { get; }
        public Description DescriptionDetails { get; }

        private Details(string name, Description description)
        {
            Name = name;
            DescriptionDetails = description;
        }

        public static Result<Details> Create(string name, Description description)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Details>("Name cannot be empty");
            }

            return new Details(name, description);
        }
    }
}
