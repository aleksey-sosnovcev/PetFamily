﻿using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public record InfoHealth
    {
        public string VValue { get; }

        private InfoHealth(string value)
        {
            VValue = value;
        }

        public static Result<InfoHealth> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<InfoHealth>("InfoHealth cannot be empty");
            }
            return new InfoHealth(value);
        }
    }
}
