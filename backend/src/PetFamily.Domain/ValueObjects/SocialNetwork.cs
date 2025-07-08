using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public record SocialNetwork
    {
        public string Link { get; }
        public string Name { get; }

        private SocialNetwork(string link, string name)
        {
            Link = link;
            Name = name;
        }

        public static Result<SocialNetwork> Create(string link, string name)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return Result.Failure<SocialNetwork>("Link cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<SocialNetwork>("Name cannot be empty");
            }

            return new SocialNetwork(link, name);
        }
    }
}
