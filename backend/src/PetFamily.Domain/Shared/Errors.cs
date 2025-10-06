using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public static class Errors
    {
        public class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for id '{id}'";
                return Error.NotFound("record.not.found", $"record not found{forId}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name == null ? "" : " " + name + " ";
                return Error.Validation("length.is.invalid", $"invalid{label}length");
            }
        }

        public static class Volunteer
        {
            public static Error AlreadyExist()
            {
                return Error.Validation("record.already.exist", "Volunteer already exist");
            }
        }

        public static class Pet
        {
            public static Error NotFound()
            {
                return Error.NotFound("record.not.found", $"pet not found");
            }
        }
    }
}
