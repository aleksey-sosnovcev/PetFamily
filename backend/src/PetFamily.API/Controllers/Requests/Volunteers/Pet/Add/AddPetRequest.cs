using PetFamily.Application.VolunteerOperations.PetOperations;
using PetFamily.Application.VolunteerOperations.PetOperations.Add;
using PetFamily.Domain.Enum;

namespace PetFamily.API.Controllers.Requests.Volunteers.Pet.Add
{
    public record AddPetRequest(
        string Name,
        Guid SpeciesId,
        string Description,
        Guid BreedId,
        string Color,
        string InfoHealth,
        string City,
        string Street,
        string HouseNumber,
        string Apartment,
        string PostalCode,
        float Weight,
        float Grouwth,
        string PhoneNumber,
        bool Castration,
        DateOnly BirthDate,
        bool Vaccination,
        StatusType Status,
        string DetailsName,
        string DetailsDescription,
        DateOnly CreateDate)
    {
        public AddPetCommand ToCommand(Guid volunteerId) =>
            new(volunteerId,
                Name,
                SpeciesId,
                Description,
                BreedId,
                Color,
                InfoHealth,
                City,
                Street,
                HouseNumber,
                Apartment,
                PostalCode,
                Weight,
                Grouwth,
                PhoneNumber,
                Castration,
                BirthDate,
                Vaccination,
                Status,
                DetailsName,
                DetailsDescription,
                CreateDate);
    }
}
