
﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Species
{

    public class Species : Shared.Entity<SpeciesId>
    {
        //EF Core
        private Species(SpeciesId id) : base(id)
        {

        }

        private readonly List<Breed> _breeds = [];
        public string Name { get; }
        public IReadOnlyList<Breed> Breeds => _breeds;

        private Species(SpeciesId speciesId, string name) : base(speciesId)
        {
            Name = name;
        }

        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }


        public static Result<Species> Create(SpeciesId speciesId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Species>("Species name cannot be empty");

            return Result.Success<Species>(new Species(speciesId, name));
        }


    }
}
