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
    public sealed class Volunteer : SoftDeletableEntity<VolunteerId>
    {
        //EF Core
        private Volunteer(VolunteerId id) : base(id)
        {

        }

        private readonly List<SocialNetwork> _socialNetworks = [];
        private readonly List<Pet> _pets = [];

        public FullName FullName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;
        public IReadOnlyList<SocialNetwork>? SocialNetworks => _socialNetworks;
        public Details Details { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets;

        public Volunteer(
            VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            PhoneNumber phoneNumber,
            Details details,
            List<SocialNetwork> socialNetworks
            ) : base(volunteerId)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            Details = details;
            _socialNetworks = socialNetworks;
        }

        public int CountPetNeedHelp()
        {
            return _pets.Where(p => p.Status == StatusType.NeedHelp).Count();
        }
        public int CountPetNeedHome()
        {
            return _pets.Where(p => p.Status == StatusType.NeedHome).Count();
        }
        public int CountPetFoundHome()
        {
            return _pets.Where(p => p.Status == StatusType.NeedHelp).Count();
        }
        public static Result<Volunteer, Error> Create(VolunteerId volunteerId,
            FullName fullName,
            Email email,
            Description description,
            PhoneNumber phoneNumber,
            Details details,
            List<SocialNetwork> socialNetworks)
        {
            var volunteer = new Volunteer(volunteerId, fullName, email, description, phoneNumber, details, socialNetworks);

            return volunteer;
        }

        public void UpdateMainInfo(FullName fullName, Description description, PhoneNumber phoneNumber)
        {
            FullName = fullName;
            Description = description;
            PhoneNumber = phoneNumber;
        }

        public void UpdateDetailsInfo(Details details)
        {
            Details = details;
        }

        public void UpdateSocialNetworksInfo(IEnumerable<SocialNetwork> socialNetworks)
        {
            _socialNetworks.Clear();
            _socialNetworks.AddRange(socialNetworks);
        }

        public override void Delete()
        {
            base.Delete();

            foreach (var pet in _pets)
                pet.Delete();
        }

        public override void Restore()
        {
            base.Restore();
        }

        public void DeleteExpiredPets()
        {
            _pets.RemoveAll(p => p.DeletionDate != null
            && DateTime.UtcNow >= p.DeletionDate.Value
            .AddDays(Constants.DELETE_EXPIRED_PETS_SERVICE_REDUCTION_HOURS));
        }

        public UnitResult<Error> AddPet(Pet pet)
        {
            var PositionResult = Position.Create(_pets.Count + 1);
            if (PositionResult.IsFailure)
                return PositionResult.Error;

            pet.SetPosition(PositionResult.Value);


            _pets.Add(pet);
            return Result.Success<Error>();
        }

        public UnitResult<Error> DeletePet(Pet pet)
        {
            var newPosition = Position.Create(_pets.Count);
            if (newPosition.IsFailure)
                return newPosition.Error;

            MovePet(pet, newPosition.Value);

            _pets.Remove(pet);
            return Result.Success<Error>();
        }

        public UnitResult<Error> MovePet(Pet pet, Position newPosition)
        {
            var currentPosition = pet.Position;
            if (currentPosition == newPosition || _pets.Count == 1)
                return Result.Success<Error>();

            var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
            if (adjustedPosition.IsFailure)
                return adjustedPosition.Error;

            newPosition = adjustedPosition.Value;

            var moveResult = MovePetsBetweenPositions(newPosition, currentPosition);
            if (moveResult.IsFailure)
                return moveResult.Error;

            pet.Move(newPosition);

            return Result.Success<Error>();
        }

        private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
        {
            if (newPosition.Value <= _pets.Count)
                return newPosition;

            var lastPosition = Position.Create(_pets.Count - 1);
            if (lastPosition.IsFailure)
                return lastPosition.Error;

            return lastPosition.Value;
        }

        private UnitResult<Error> MovePetsBetweenPositions(Position newPosition, Position currentPosition)
        {
            if (newPosition.Value < currentPosition.Value)
            {
                var petsToMove = _pets.Where(p => p.Position.Value >= newPosition.Value
                && p.Position.Value < currentPosition.Value);

                foreach (var petToMove in petsToMove)
                {
                    var result = petToMove.MoveForward();
                    if (result.IsFailure)
                        return result.Error;
                }
            }
            else if (newPosition.Value > currentPosition.Value)
            {
                var petsToMove = _pets.Where(p => p.Position.Value > currentPosition.Value
                && p.Position.Value <= newPosition.Value);

                foreach (var petToMove in petsToMove)
                {
                    var result = petToMove.MoveBack();
                    if (result.IsFailure)
                        return result.Error;
                }
            }
            return Result.Success<Error>();
        }
    }
}
