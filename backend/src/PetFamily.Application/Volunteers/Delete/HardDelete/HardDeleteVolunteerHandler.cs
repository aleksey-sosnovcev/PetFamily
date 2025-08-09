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

namespace PetFamily.Application.Volunteers.Delete.HardDelete
{
    public class HardDeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;

        public HardDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<HardDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            DeleteVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Volunteer deleted with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
