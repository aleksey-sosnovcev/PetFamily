using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.ValueObjects
{
    public record HelpStatus
    {
        public const int MAX_VALUE_LENGTH = 20;

        public static readonly HelpStatus NeedsHelp = new(nameof(NeedsHelp));
        public static readonly HelpStatus NeedsHome = new(nameof(NeedsHome));
        public static readonly HelpStatus FoundsHome = new(nameof(FoundsHome));

        private static readonly HelpStatus[] _all = [NeedsHelp, NeedsHome, FoundsHome];

        public string Value { get; }

        private HelpStatus(string value)
        {
            Value = value;
        }

        public static Result<HelpStatus, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsRequired("HelpStatus");

            var valueToLower = value.Trim().ToLower();

            if(_all.Any(h => h.Value.ToLower() == valueToLower) == false)
                return Errors.General.ValueIsInvalid(valueToLower);

            return new HelpStatus(value);
        }
    }
}
