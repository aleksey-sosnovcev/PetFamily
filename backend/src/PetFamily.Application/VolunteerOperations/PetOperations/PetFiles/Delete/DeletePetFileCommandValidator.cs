using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Delete
{
    public class DeletePetFileCommandValidator : AbstractValidator<DeletePetFileCommand>
    {
        public DeletePetFileCommandValidator()
        {
            RuleFor(p => p.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.PetId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => new { p.Files })
                .MustBeValueObject(f => FilePath.Create(f.Files));
        }
    }
}
