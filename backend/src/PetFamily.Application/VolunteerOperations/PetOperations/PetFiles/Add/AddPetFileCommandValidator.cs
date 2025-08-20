using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add
{
    public class AddPetFileCommandValidator : AbstractValidator<AddPetFileCommand>
    {
        public AddPetFileCommandValidator() 
        {
            RuleFor(p => p.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.PetId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleForEach(p => p.Files).ChildRules(p =>
            {
                p.RuleFor(x => x.Stream)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired("Stream"));

                p.RuleFor(x => x.FileName)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired("FileName"));
            });
        }
    }
}
