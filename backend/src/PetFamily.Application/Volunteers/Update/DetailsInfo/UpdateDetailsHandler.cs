using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update.MainInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.DetailsInfo
{
    public class UpdateDetailsHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateDetailsHandler> _logger;

        public UpdateDetailsHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateDetailsHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(
            UpdateDetailsRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var detailsResult = Details.Create(request.Details.Name, request.Details.Description).Value;

            volunteerResult.Value.UpdateDetailsInfo(detailsResult);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's details info with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
