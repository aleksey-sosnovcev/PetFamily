using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain
{
    public record SocialNetwork
    {
        public string Link { get; }
        public string Name { get; }

        private SocialNetwork(string link,string name)
        {
            Link = link;
            Name = name;
        }

<<<<<<< Updated upstream:backend/src/PetFamily.Domain/SocialNetwork.cs
        public static Result<SocialNetwork> Create(string link,string name)
=======
        public static Result<SocialNetwork, Error> Create(string link, string name)
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/ValueObjects/SocialNetwork.cs
        {
            if(string.IsNullOrWhiteSpace(link))
            {
                return Errors.General.ValueIsInvalid("Link");
            }
            if(string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsInvalid("Name");
            }

            var socialNetwork = new SocialNetwork(link, name);

            return socialNetwork;
        }
    }
}
