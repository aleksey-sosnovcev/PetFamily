using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain
{
    public record Email
    {
        public static string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public string EmailAddress { get; }

        private Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Email.cs
        public static Result<Email> Create(string emailAddress)
=======
        public static Result<Email, Error> Create(string value)
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/ValueObjects/Email.cs
        {
            if (!Regex.IsMatch(emailAddress, emailPattern, RegexOptions.IgnoreCase))
            {
                return Errors.General.ValueIsInvalid("Email");
            }

<<<<<<< Updated upstream:backend/src/PetFamily.Domain/Email.cs
            return new Email(emailAddress);
=======
            var email = new Email(value);

            return email;
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/ValueObjects/Email.cs
        }

    }
}
