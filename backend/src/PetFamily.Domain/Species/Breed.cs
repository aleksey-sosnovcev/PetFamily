﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species
{
    public class Breed : Shared.Entity<BreedId>
    {
        private Breed(BreedId id) : base(id)
        {
            
        }
        public string Name { get; private set; } = default!;
        public Breed(BreedId breedId, string name) : base(breedId) 
        {
            Name = name;
        }
        public static Result<Breed, Error> Create(BreedId breedId, string name)
        {
            if (string.IsNullOrEmpty(name))
                return Errors.General.ValueIsInvalid("Name");

            var breed = new Breed(breedId, name);

            return breed;
        }
    }
}
