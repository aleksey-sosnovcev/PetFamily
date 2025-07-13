namespace PetFamily.Domain.ValueObjects
{
    public record SocialNetworkDetails
    {
        private readonly List<SocialNetwork> _socialNetworks = [];

        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

        public SocialNetworkDetails(List<SocialNetwork> socialNetworks)
        {
            _socialNetworks = socialNetworks;
        }

        private SocialNetworkDetails()
        {
            
        }
    }
}
