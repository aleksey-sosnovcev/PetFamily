using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Application.SpeciesOperations;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.Add
{
    public class AddPetHandler
    {

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IValidator<AddPetCommand> _validator;
        private readonly ILogger<AddPetHandler> _logger;

        public AddPetHandler(
            IVolunteerRepository volunteerRepository,
            ISpeciesRepository speciesRepository,
            IValidator<AddPetCommand> validator,
            ILogger<AddPetHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _speciesRepository = speciesRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            AddPetCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ErrorList();
            }

            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var speciesId = SpeciesId.Create(command.SpeciesId);
            var breedId = BreedId.Create(command.BreedId);

            //var speciesAndBreedCheck = await _speciesRepository.GetBySpeciesAndBreed(
            //    speciesId,
            //    breedId,
            //    cancellationToken);

            //if (speciesAndBreedCheck.IsFailure)
            //    return speciesAndBreedCheck.Error.ToErrorList();

            var petId = PetId.NewPetId();
            var name = Name.Create(command.Name).Value;
            var speciesAndBreed = SpeciasAndBreed.Create(speciesId, breedId).Value;
            var description = Description.Create(command.Description).Value;
            var color = command.Color;
            var infoHealth = InfoHealth.Create(command.InfoHealth).Value;
            var address = Address.Create(
                command.City,
                command.Street,
                command.HouseNumber,
                command.Apartment,
                command.PostalCode).Value;
            var weigth = command.Weight;
            var grouwth = command.Grouwth;
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            var castration = command.Castration;
            var birthDate = command.BirthDate;
            var vaccination = command.Vaccination;
            var status = command.Status;
            var details = Details.Create(command.DetailsName, command.DetailsDescription).Value;
            var createDate = command.CreateDate;

            var pet = Pet.Create(
                petId,
                name,
                speciesAndBreed,
                description,
                color,
                infoHealth,
                address,
                weigth,
                grouwth,
                phoneNumber,
                castration,
                birthDate,
                vaccination,
                status,
                details,
                createDate);


            var addResult = volunteerResult.Value.AddPet(pet.Value);
            if (addResult.IsFailure)
                return addResult.Error.ToErrorList();

            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("added pet with id {petId} to volunteer with id {volunteerId}", petId.Value, command.VolunteerId);

            return petId.Value;
        }

    }
}
