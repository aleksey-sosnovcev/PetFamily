using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Database
{
    public interface IReadDbContext
    {
        IQueryable<VolunteerDto> Volunteers { get; }
    }
}
