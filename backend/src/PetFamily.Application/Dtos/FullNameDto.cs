using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public record FullNameDto(
       string Surname,
       string FirstName,
       string Patronymic);
}
