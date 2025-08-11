using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.ValueObjects
{
    public record SerialNumber
    {
        private SerialNumber(int value)
        {
            Value = value;
        }
        public int Value { get; }

        public static Result<SerialNumber, Error> Create(int number)
        {
            if (number <= 0)
                return Errors.General.ValueIsInvalid("Serial number");

            return new SerialNumber(number);
        }
    }
}
