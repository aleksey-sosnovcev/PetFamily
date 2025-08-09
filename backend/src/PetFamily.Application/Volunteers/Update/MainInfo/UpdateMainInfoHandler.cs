using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(
            UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var fullNameResult = FullName.Create(
                request.Dto.FullName.Surname,
                request.Dto.FullName.FirstName,
                request.Dto.FullName.Patronymic).Value;

            var descriptionResut = Description.Create(request.Dto.Description).Value;

            var phoneNumberResult = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

            volunteerResult.Value.UpdateMainInfo(
                fullNameResult,
                descriptionResut,
                phoneNumberResult);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's main info with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
