using CSharpFunctionalExtensions;
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

            var fullNameResult = FullName.Create(request.FullName.surName, request.FullName.firstName, request.FullName.patronymic);
            if (fullNameResult.IsFailure)
                return fullNameResult.Error;

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var descriptionResut = Description.Create(request.Description);
            if (descriptionResut.IsFailure)
                return descriptionResut.Error;

            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumberResult.IsFailure)
                return phoneNumberResult.Error;

            var detailsResult = Details.Create(request.Details.Name, request.Details.Description);
            if (detailsResult.IsFailure)
                return detailsResult.Error;

            var socialNetworkResult = request.SocialNetworks
                .Select(s => SocialNetwork.Create(s.Link, s.Name))
                .ToList();
            foreach (var s in socialNetworkResult)
            {
                if (s.IsFailure)
                    return s.Error;
            }


            var volunteer = await _volunteerRepository.GetByEmail(emailResult.Value);
            if (volunteer.IsSuccess)
                return Errors.Volunteer.AlreadyExist();


            var volunteerResult = Volunteer.Create(
                volunteerId,
                fullNameResult.Value,
                emailResult.Value,
                descriptionResut.Value,
                phoneNumberResult.Value,
                detailsResult.Value);

            await _volunteerRepository.Add(volunteerResult.Value, cancellationToken);

            return volunteerId.Value;
        }
    }
}
