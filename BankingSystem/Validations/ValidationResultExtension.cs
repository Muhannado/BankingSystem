using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankingSystem.Validations;

public static class ValidationResultExtension
{
    public static ModelStateDictionary GetModelStateDictionary(this ValidationResult validationResult)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var failure in validationResult.Errors)
        {
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
        }

        return modelStateDictionary;
    }
}