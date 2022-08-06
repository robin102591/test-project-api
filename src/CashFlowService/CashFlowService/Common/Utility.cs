using CashFlowService.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace CashFlowService.Common
{
    public static class Utility
    {
        public static ErrorResponse GetRequestModelErrors(this ModelStateDictionary modelState)
        {
            var errors = new ErrorResponse();

            var errorDetails = new List<string>();

            var errorList = modelState.Where(m => m.Value.Errors?.Any(e => !string.IsNullOrEmpty(e.ErrorMessage)) == true);

            if (!errorList.Any())
            {
                errors = new ErrorResponse
                {
                    Message = "Invalid request"
                };

                return errors;
            }

            foreach (var modelStateEntry in errorList)
            {
                foreach (var error in modelStateEntry.Value.Errors)
                {
                    errorDetails.Add($"{modelStateEntry.Key} : {error.ErrorMessage}");                    
                }
            }

            errors = new ErrorResponse
            {
                Message = "Invalid request",
                Errors = errorDetails.ToArray()
            };

            return errors;
        }
    }
}
