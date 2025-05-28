namespace PetFamily.Domain.ValueObjects
{
    public record SocialNetworkDetails
    {
        private readonly List<SocialNetwork> _socialNetworks = [];

        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    }
}
