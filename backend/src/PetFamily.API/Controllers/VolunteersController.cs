using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.VolunteerOperations.Create;
using PetFamily.Application.VolunteerOperations.Delete;
using PetFamily.Application.VolunteerOperations.Delete.HardDelete;
using PetFamily.Application.VolunteerOperations.Delete.SoftDelete;
using PetFamily.Application.VolunteerOperations.Dtos;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Application.VolunteerOperations.Update.MainInfo;
using PetFamily.Application.VolunteerOperations.Update.SocialNetworks;
using PetFamily.API.Controllers.Requests;
using PetFamily.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PetFamily.Domain.ValueObjects;
using System.Collections.Generic;
using PetFamily.API.Controllers.Requests.Volunteers.Update;
using PetFamily.API.Controllers.Requests.Volunteers.Create;
using CSharpFunctionalExtensions;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add;
using PetFamily.API.Processors;
using PetFamily.API.Controllers.Requests.Volunteers.DeletePetFile;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Delete;
using PetFamily.Application.VolunteerOperations.PetOperations.Add;
using PetFamily.API.Controllers.Requests.Volunteers.Pet.Add;
using PetFamily.API.Controllers.Requests.Volunteers.Pet.Move;
using PetFamily.Application.VolunteerOperations.PetOperations.Move;

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

        [HttpPost("{id:guid}/pet")]
        public async Task<ActionResult> AddPet(
            [FromRoute] Guid id,
            [FromServices] AddPetHandler handler,
            [FromForm] AddPetRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddPetCommand(
                id,
                request.Name,
                request.SpeciesId,
                request.Description,
                request.BreedId,
                request.Color,
                request.InfoHealth,
                request.City,
                request.Street,
                request.HouseNumber,
                request.Apartment,
                request.PostalCode,
                request.Weight,
                request.Grouwth,
                request.PhoneNumber,
                request.Castration,
                request.BirthDate,
                request.Vaccination,
                request.Status,
                request.DetailsName,
                request.DetailsDescription,
                request.CreateDate);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photos")]
        public async Task<ActionResult> AddPetFile(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            IFormFileCollection files,
            [FromServices] AddPetFileHandler handler,
            CancellationToken cancellationToken)
        {
            var fileProcessor = new FormFileProcessor();
            var fileDtos = fileProcessor.Process(files);

            var command = new AddPetFileCommand(volunteerId, petId, fileDtos);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photos")]
        public async Task<ActionResult> DeletePetFile(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromBody] DeletePetFileRequest request,
            [FromServices] DeletePetFileHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeletePetFileCommand(volunteerId, petId, request.FileName);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        }

        [HttpPut("{volunteerId:guid}/pet/{petId:guid}/position")]
        public async Task<ActionResult> MovePet(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromForm] MovePetRequest request,
            [FromServices] MovePetHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new MovePetCommand(volunteerId, petId, request.NewPosition);

            var result = await handler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            var envelope = Envelope.Ok(result.Value);

            return Ok(envelope);
        } 
    }
}
