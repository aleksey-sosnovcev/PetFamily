namespace PetFamily.Domain.ValueObjects
{
    public record SocialNetworkDetails
    {
        public IReadOnlyList<SocialNetwork> SocialNetworks { get; } = [];

        public SocialNetworkDetails(List<SocialNetwork> socialNetworks)
        {
            SocialNetworks = socialNetworks;
        }

        private SocialNetworkDetails()
        {
            
        }
    }
}
