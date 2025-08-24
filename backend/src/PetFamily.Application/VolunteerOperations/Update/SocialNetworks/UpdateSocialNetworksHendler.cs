using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Update.SocialNetworks
{
    public class UpdateSocialNetworksHendler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateSocialNetworksCommand> _validator;
        private readonly ILogger<UpdateSocialNetworksHendler> _logger;

        public UpdateSocialNetworksHendler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateSocialNetworksCommand> validator,
            ILogger<UpdateSocialNetworksHendler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateSocialNetworksCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            var socialNetworks = new List<SocialNetwork>(command.SocialNetworks.Count());

            socialNetworks.AddRange(from s in command.SocialNetworks
                                    let socialResult = SocialNetwork.Create(s.Link, s.Name)
                                    select socialResult.Value);

            volunteerResult.Value.UpdateSocialNetworksInfo(socialNetworks);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's details info with id {volunteerId}", command.VolunteerId);

            return result;
        }
    }
}
