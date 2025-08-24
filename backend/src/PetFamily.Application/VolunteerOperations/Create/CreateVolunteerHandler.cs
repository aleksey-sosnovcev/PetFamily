using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Create
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<CreateVolunteerCommand> validator,
            ILogger<CreateVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            CreateVolunteerCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerId = VolunteerId.NewVolunteerId();

            var fullNameResult = FullName.Create(
                command.Surname,
                command.FirstName,
                command.Patronymic).Value;

            var emailResult = Email.Create(command.Email).Value;

            var descriptionResut = Description.Create(command.Description).Value;

            var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber).Value;

            var detailsResult = Details.Create(command.DetailsName, command.DetailsDescription).Value;

            var socialNetworks = new List<SocialNetwork>(command.SocialNetworks.Count());

            socialNetworks.AddRange(from s in command.SocialNetworks
                                    let socialResult = SocialNetwork.Create(s.Link, s.Name)
                                    select socialResult.Value);

            var volunteer = await _volunteerRepository.GetByEmail(emailResult, cancellationToken);
            if (volunteer.IsSuccess)
                return Errors.Volunteer.AlreadyExist().ToErrorList();


            var volunteerResult = Volunteer.Create(
                volunteerId,
                fullNameResult,
                emailResult,
                descriptionResut,
                phoneNumberResult,
                detailsResult,
                socialNetworks);

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId.Value);

            return volunteerId.Value;
        }
    }
}
