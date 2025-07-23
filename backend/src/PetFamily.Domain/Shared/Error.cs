using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public record Error
    {
        public const string SEPARATOR = "||";
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

        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public static Error Deserialize(string serialized)
        {
            var parts = serialized.Split(SEPARATOR);

            if (parts.Length < 3)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            if (System.Enum.TryParse<ErrorType>(parts[2], out var type) == false)
            {
                throw new ArgumentException("Invalid serialized format");
            }

            return new Error(parts[0], parts[1], type);
        }

    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}


