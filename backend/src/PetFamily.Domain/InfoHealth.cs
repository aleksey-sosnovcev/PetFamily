using CSharpFunctionalExtensions;

namespace PetFamily.Domain
{
    public record InfoHealth
    {
        public Description Description { get; }

        private InfoHealth(Description description)
        {
            Description = description;
        }

        public static Result<InfoHealth> Create(Description description)
        {
            return new InfoHealth(description);
        }
    }
}
