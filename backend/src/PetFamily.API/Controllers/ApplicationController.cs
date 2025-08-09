using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.Create;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApplicationController : ControllerBase
    {

    }
}
