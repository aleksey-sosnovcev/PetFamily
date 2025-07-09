using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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

        public static Result<SocialNetwork, Error> Create(string link, string name)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return Errors.General.ValueIsInvalid("Link");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsInvalid("Name");
            }

            var socialNetwork = new SocialNetwork(link, name);

            return socialNetwork;
        }
    }
}
