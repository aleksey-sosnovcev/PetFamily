using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.IdVO;

namespace PetFamily.Domain.Species
{
    internal class Breed
    {
        
        public BreedId Id { get; }
        public string Name { get; }
        private Breed(string name) 
        {
            Name = name;
        }
        public static Result<Breed> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Breed>("Name cannot be empty");

            return Result.Success<Breed>(new Breed(name));
        }
    }
}
