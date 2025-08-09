using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.Delete.HardDelete;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Delete.SoftDelete
{
    public class SoftDeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

        public SoftDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<SoftDeleteVolunteerHandler> logger)
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

            volunteerResult.Value.Delete();

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Volunteer deleted with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
