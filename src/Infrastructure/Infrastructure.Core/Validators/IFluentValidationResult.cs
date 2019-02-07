using System.Collections.Generic;
using FluentValidation.Results;

namespace Infrastructure.Core.Validators
{
    public interface IFluentValidationResult
    {
        IDictionary<string, List<string>> Errors { get; }

        bool IsValid { get; }

        void AddModelError(IEnumerable<ValidationFailure> failures);

        void AddModelError(string errorMessage);

        void AddModelError(string key, string errorMessage);
    }
}