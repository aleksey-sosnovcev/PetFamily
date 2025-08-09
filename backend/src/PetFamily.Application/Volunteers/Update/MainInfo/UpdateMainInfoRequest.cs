using FluentValidation.AspNetCore;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public record UpdateMainInfoRequest(Guid VolunteerId, UpdateMainInfoDto Dto);

    public record UpdateMainInfoDto(
        FullNameDto FullName,
        string Description,
        string PhoneNumber);
}
