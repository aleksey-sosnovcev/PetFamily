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
using PetFamily.API.Controllers.Requests;
using PetFamily.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PetFamily.Domain.ValueObjects;
using System.Collections.Generic;
using PetFamily.API.Controllers.Requests.Volunteers.Update;
using PetFamily.API.Controllers.Requests.Volunteers.Create;

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
            var command = new CreateVolunteerCommand(
                request.Surname,
                request.FirstName,
                request.Patronymic,
                request.Email,
                request.Description,
                request.PhoneNumber,
                request.DetailsName,
                request.DetailsDescription,
                request.SocialNetworks);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateMainInfoCommand(
                id,
                request.Surname,
                request.FirstName,
                request.Patronymic,
                request.Description,
                request.PhoneNumber);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/details-info")]
        public async Task<ActionResult> UpdateDetailsInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsHandler handler,
            [FromBody] UpdateDetailsRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateDetailsCommand(
                id,
                request.Name,
                request.Description);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{id:guid}/social-networks-info")]
        public async Task<ActionResult> UpdateSocialNetworksInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworksHendler handler,
            [FromBody] UpdateSocialNetworksRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateSocialNetworksCommand(
                id,
                request.SocialNetworks);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> HardDelete(
            [FromRoute] Guid id,
            [FromServices] HardDeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVolunteerCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpDelete("{id:guid}/deactivation")]
        public async Task<ActionResult> SoftDelete(
            [FromRoute] Guid id,
            [FromServices] SoftDeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVolunteerCommand(id);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }
    }
}
