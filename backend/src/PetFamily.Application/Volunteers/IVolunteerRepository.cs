using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteerRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId);
        Task<Result<Volunteer, Error>> GetByEmail(Email email);
    }
}
