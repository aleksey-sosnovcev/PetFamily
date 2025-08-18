using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations
{
    public record AddPetCommand(
        Guid VolunteerId,
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
        string Status,
        string DetailsName,
        string DetailsDescription,
        DateOnly CreateDate);
}
