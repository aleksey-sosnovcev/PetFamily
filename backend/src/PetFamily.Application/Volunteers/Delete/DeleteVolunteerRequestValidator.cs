using FluentValidation;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty();
        }
    }
}
