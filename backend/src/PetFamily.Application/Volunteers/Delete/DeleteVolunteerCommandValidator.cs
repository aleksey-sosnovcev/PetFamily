using FluentValidation;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
    {
        public DeleteVolunteerCommandValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
