using PetFamily.Application.VolunteerOperations.PetOperations;
using PetFamily.Domain.Enum;

namespace PetFamily.API.Controllers.Requests.Volunteers.AddPet
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
        DateOnly CreateDate);
}
