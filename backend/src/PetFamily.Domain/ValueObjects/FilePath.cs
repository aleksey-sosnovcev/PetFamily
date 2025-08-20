using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.ValueObjects
{
    public record FilePath
    {
        private FilePath(string pathToStorage)
        {
            PathToStorage = pathToStorage;
        }

        public string PathToStorage { get; }

        public static Result<FilePath, Error> Create(string pathToStorage)
        {
            if (string.IsNullOrWhiteSpace(pathToStorage))
                return Errors.General.ValueIsInvalid("PathToStorage");

            return new FilePath(pathToStorage);
        }
    }
}
