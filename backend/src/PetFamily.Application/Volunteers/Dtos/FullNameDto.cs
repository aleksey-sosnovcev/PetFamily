using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Dtos
{
    public record FullNameDto(
       string surName,
       string firstName,
       string patronymic);
}
