using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Enum;
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Volunteer.cs
using PetFamily.Domain.IdVO;
=======
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Pets;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Volunteer/Volunteer.cs

namespace PetFamily.Domain
{
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Volunteer.cs
    internal class Volunteer
=======
    public sealed class Volunteer : Shared.Entity<VolunteerId>
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Volunteer/Volunteer.cs
    {
        //EF Core
        private Volunteer()
        {

        }
        private readonly List<SocialNetwork> socialNetworks = [];
        private readonly List<Pet> _pets = [];

        public VolunteerId Id { get; private set; }
        public FullName FullName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Volunteer.cs
        public IReadOnlyList<SocialNetwork> SocialNetworks => socialNetworks;
=======
        public SocialNetworkDetails? SocialNetworkDetails { get; private set; } = default!;
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Volunteer/Volunteer.cs
        public Details Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets; 

        public Volunteer(
            VolunteerId id, 
            FullName fullName, 
            Email email, 
            Description description, 
            PhoneNumber phoneNumber,
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Volunteer.cs
            Details details)
=======
            Details details
            ) : base(volunteerId)
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Volunteer/Volunteer.cs
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            Details = details;
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
            Details details)
        {
            var volunteer = new Volunteer(volunteerId, fullName, email, description, phoneNumber, details);

            return volunteer;
        }
    }
}
