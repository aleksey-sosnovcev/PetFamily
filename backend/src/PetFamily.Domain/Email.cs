using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

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

        public static Result<Email> Create(string emailAddress)
        {
            if (!Regex.IsMatch(emailAddress, emailPattern, RegexOptions.IgnoreCase))
            {
                return Result.Failure<Email>("EmailAddress does not match the form");
            }

            return new Email(emailAddress);
        }

    }
}
