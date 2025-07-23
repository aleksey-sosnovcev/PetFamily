using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Pets;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;


namespace PetFamily.Domain.Volunteers
{
    public sealed class Volunteer : Shared.Entity<VolunteerId>
    {
        //EF Core
        private Volunteer(VolunteerId id) : base(id) 
        {

        }

        private readonly List<Pet> _pets = [];

        public FullName FullName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public SocialNetworkDetails? SocialNetworkDetails { get; private set; } = default!;
        public Details Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;

        public Volunteer(
            VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            PhoneNumber phoneNumber,
            Details details,
            SocialNetworkDetails socialNetworkDetails
            ) : base(volunteerId)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            Details = details;
            SocialNetworkDetails = socialNetworkDetails;
        }

        public int CountPetNeedHelp()
        {
            return Pets.Where(p => p.Status == StatusType.NeedHelp).Count();
        }
        public int CountPetNeedHome()
        {
            return Pets.Where(p => p.Status == StatusType.NeedHome).Count();
        }
        public int CountPetFoundHome()
        {
            return Pets.Where(p => p.Status == StatusType.NeedHelp).Count();
        }

        public static Result<Volunteer, Error> Create(VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            PhoneNumber phoneNumber,
            Details details,
            List<SocialNetwork> socialNetwork)
        {
            var socialNetworkDetails = new SocialNetworkDetails(socialNetwork);

            var volunteer = new Volunteer(volunteerId, fullName, email, description, phoneNumber, details, socialNetworkDetails);

            return volunteer;
        }
    }
}
