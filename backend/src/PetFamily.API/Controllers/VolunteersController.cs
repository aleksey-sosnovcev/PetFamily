using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.Delete.HardDelete;
using PetFamily.Application.Volunteers.Delete.SoftDelete;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update.DetailsInfo;
using PetFamily.Application.Volunteers.Update.MainInfo;
using PetFamily.Application.Volunteers.Update.SocialNetworks;
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
            var result = await handler.Handle(request, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/details-info")]
        public async Task<ActionResult> UpdateDetailsInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsHandler handler,
            [FromBody] DetailsDto dto,
            [FromServices] IValidator<UpdateDetailsRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateDetailsRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/social-networks-info")]
        public async Task<ActionResult> UpdateSocialNetworksInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworksHendler handler,
            [FromBody] UpdateSocialNetworksDto dto,
            [FromServices] IValidator<UpdateSocialNetworksRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateSocialNetworksRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> HardDelete(
            [FromRoute] Guid id,
            [FromServices] HardDeleteVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteVolunteerRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpDelete("{id:guid}/deactivation")]
        public async Task<ActionResult> SoftDelete(
            [FromRoute] Guid id,
            [FromServices] SoftDeleteVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteVolunteerRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }
    }
}
