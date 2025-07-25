﻿using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }
        public async Task<Result<Guid, Error>> Handel(
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullNameResult = FullName.Create(
                request.FullName.Surname,
                request.FullName.FirstName, 
                request.FullName.Patronymic).Value;

            var emailResult = Email.Create(request.Email).Value;

            var descriptionResut = Description.Create(request.Description).Value;

            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber).Value;

            var detailsResult = Details.Create(request.Details.Name, request.Details.Description).Value;

            var socialNetworks = new List<SocialNetwork>(request.SocialNetworks.Count());

            socialNetworks.AddRange(from s in request.SocialNetworks
                                    let socialResult = SocialNetwork.Create(s.Link, s.Name)
                                    select socialResult.Value);

            var volunteer = await _volunteerRepository.GetByEmail(emailResult);
            if (volunteer.IsSuccess)
                return Errors.Volunteer.AlreadyExist();


            var volunteerResult = Volunteer.Create(
                volunteerId,
                fullNameResult,
                emailResult,
                descriptionResut,
                phoneNumberResult,
                detailsResult,
                socialNetworks);

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            return volunteerId.Value;
        }
    }
}
