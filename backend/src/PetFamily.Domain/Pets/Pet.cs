using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Pets
{
    public class Pet : Shared.Entity<PetId>
    {
        //EF Core
        private Pet(PetId id) : base(id)
        {

        }

        public Name Name { get; private set; } = default!;
        public SpeciasAndBreed SpeciasAndBreed { get; private set; } = default!;
        public string Species { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public string Breed { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public InfoHealth InfoHealth { get; private set; } = default!;
        public Address Address { get; private set; } = default!;
        public float Weight { get; private set; }
        public float Growth { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public bool Castration { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public bool Vaccination { get; private set; }
        public StatusType Status { get; private set; }
        public Details Details { get; private set; } = default!;
        public DateOnly CreateDate { get; private set; }

        public Pet(
            PetId petId,
            Name name,
            string species,
            Description description,
            string breed,
            string color,
            InfoHealth infoHealth,
            Address address,
            float weight,
            float growth,
            PhoneNumber phoneNumber,
            bool castration,
            DateOnly birthDate,
            bool vaccination,
            StatusType status,
            Details details,
            DateOnly createDateTest) : base(petId)
        {
            Name = name;
            Species = species;
            Description = description;
            Breed = breed;
            Color = color;
            InfoHealth = infoHealth;
            Address = address;
            Weight = weight;
            Growth = growth;
            PhoneNumber = phoneNumber;
            Castration = castration;
            BirthDate = birthDate;
            Vaccination = vaccination;
            Status = status;
            Details = details;
            CreateDate = createDateTest;
        }
    }
}
