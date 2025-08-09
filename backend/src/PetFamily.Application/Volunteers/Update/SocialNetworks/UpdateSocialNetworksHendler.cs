using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.SocialNetworks
{
    public class UpdateSocialNetworksHendler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateSocialNetworksHendler> _logger;

        public UpdateSocialNetworksHendler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateSocialNetworksHendler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(
            UpdateSocialNetworksRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var socialNetworks = new List<SocialNetwork>(request.Dto.SocialNetworks.Count());

            socialNetworks.AddRange(from s in request.Dto.SocialNetworks
                                    let socialResult = SocialNetwork.Create(s.Link, s.Name)
                                    select socialResult.Value);

            volunteerResult.Value.UpdateSocialNetworksInfo(socialNetworks);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's details info with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
