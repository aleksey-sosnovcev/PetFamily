using FluentValidation;

namespace PetFamily.Application.VolunteerOperations.Commands.Delete
{
    public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
    {
        public DeleteVolunteerCommandValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
