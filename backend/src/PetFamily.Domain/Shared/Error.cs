using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }


        private Error(string code, string massage, ErrorType type)
        {
            Code = code;
            Message = massage;
            Type = type;
        }

        public static Error Validation(string code, string massage) =>
            new Error(code, massage, ErrorType.Validation);

        public static Error NotFound(string code, string massage) =>
            new Error(code, massage, ErrorType.NotFound);

        public static Error Failure(string code, string massage) =>
            new Error(code, massage, ErrorType.Failure);

        public static Error Conflict(string code, string massage) =>
            new Error(code, massage, ErrorType.Conflict);
    }


}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}
