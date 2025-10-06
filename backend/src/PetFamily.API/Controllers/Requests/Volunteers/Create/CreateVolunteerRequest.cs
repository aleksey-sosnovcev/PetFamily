using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerOperations.Commands.Create;

namespace PetFamily.API.Controllers.Requests.Volunteers.Create
{
    public record CreateVolunteerRequest(
       string Surname,
       string FirstName,
       string Patronymic,
       string Email,
       string Description,
       string PhoneNumber,
       string DetailsName,
       string DetailsDescription,
       IEnumerable<SocialNetworksDto> SocialNetworks)
    {
        public CreateVolunteerCommand ToCommand() =>
            new(Surname,
                FirstName, 
                Patronymic, 
                Email, 
                Description, 
                PhoneNumber, 
                DetailsName, 
                DetailsDescription, 
                SocialNetworks);
    }
}
