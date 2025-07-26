using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PetFamily.API.Controllers
{
    public class VolunteersController : ApplicationController
    {
        /*
        private readonly CreateVolunteerHandler _handler;
        public VolunteersController(CreateVolunteerHandler handler)
        {
            _handler = handler;
        }*/  //injection, неудобно когда куча методов(будет грузиться куча зависимостей)

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {

            //вызвать сервис для создания волонтера (вызов бизнес логики)
            var result = await handler.Handel(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

    }
}
