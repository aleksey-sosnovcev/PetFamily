using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;

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
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            //вызвать сервис для создания волонтера (вызов бизнес логики)

            var result = await handler.Handel(request, cancellationToken);

            return result.ToResponse();
        }
 
    }
}
