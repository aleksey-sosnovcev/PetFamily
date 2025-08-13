using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.ValueObjects
{
    public record Position
    {
        private Position(int value)
        {
            Value = value;
        }
        public int Value { get; }

        public Result<Position, Error> Forward()
            => Create(Value + 1);

        public Result<Position, Error> Back()
            => Create(Value - 1);

        public static Result<Position, Error> Create(int number)
        {
            if (number <= 0)
                return Errors.General.ValueIsInvalid("Serial number");

            return new Position(number);
        }
    }
}
