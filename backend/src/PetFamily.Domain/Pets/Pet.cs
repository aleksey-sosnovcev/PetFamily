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
    public class Pet : SoftDeletableEntity<PetId>
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
        public Position Position { get; private set; } = default!;

        public Pet(
            PetId petid,
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
            DateOnly createDate) : base(petid)
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
            CreateDate = createDate;
        }

        public static Result<Pet, Error> Create(PetId petId,
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
            DateOnly createDate)
        {

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            if (string.IsNullOrWhiteSpace(species))
            {
                return Errors.General.ValueIsInvalid("Species");
            }
            if (string.IsNullOrWhiteSpace(breed))
            {
                return Errors.General.ValueIsInvalid("Breed");
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                return Errors.General.ValueIsInvalid("Color");
            }
            if (weight <= 0)
            {
                return Errors.General.ValueIsInvalid("Weight");
            }
            if (growth <= 0)
            {
                return Errors.General.ValueIsInvalid("Growth");
            }
            if (birthDate > today.AddYears(-50))
            {
                return Errors.General.ValueIsInvalid("BirthDate");
            }

            var pet = new Pet(
                petId,
                name,
                species,
                description,
                breed,
                color,
                infoHealth,
                address,
                weight,
                growth,
                phoneNumber,
                castration,
                birthDate,
                vaccination,
                status,
                details,
                createDate);

            return pet;
        }

        public void SetPosition(Position position) =>
            Position = position;

        public UnitResult<Error> MoveForward()
        {
            var newPosition = Position.Forward();
            if (newPosition.IsFailure)
                return newPosition.Error;

            Position = newPosition.Value;

            return Result.Success<Error>();
        }

        public UnitResult<Error> MoveBack()
        {
            var newPosition = Position.Back();
            if (newPosition.IsFailure)
                return newPosition.Error;

            Position = newPosition.Value;

            return Result.Success<Error>();
        }

        public void Move(Position newPosition)
            => Position = newPosition;

        public override void Delete()
        {
            base.Delete();
        }

        public override void Restore()
        {
            base.Restore();
        }
    }
}
