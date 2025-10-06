using CSharpFunctionalExtensions;
using FluentValidation.Results;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Extensions
{
    public static class ValidationErrorsExtentions
    {
        public static ErrorList ToErrorList(this ValidationResult validationResult)
        {
            var validationErrors = validationResult.Errors;
            
            var responseErrors = from validationError in validationErrors
                                 let errorMessage = validationError.ErrorMessage
                                 let error = Error.Deserialize(errorMessage)
                                 select Error.Validation(error.Code, error.Message, validationError.PropertyName);

            return responseErrors.ToList();
        }
    }
}
