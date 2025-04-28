using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Enum;
using PetFamily.Domain.IdVO;

namespace PetFamily.Domain
{
    internal class Volunteer
    {
        //EF Core
        private Volunteer()
        {

        }

        private readonly List<Pet> _pets = [];

        public VolunteerId Id { get; private set; }
        public FullName FullName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public List<SocialNetwork> socialNetworks { get; private set; } = default!;
        public Details Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets; 

        public Volunteer(
            VolunteerId id, 
            FullName fullName, 
            Email email, 
            Description description, 
            PhoneNumber phoneNumber,
            Details details)
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
    }
}
