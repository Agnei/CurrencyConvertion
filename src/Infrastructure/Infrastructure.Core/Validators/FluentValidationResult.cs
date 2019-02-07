using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;

namespace Infrastructure.Core.Validators
{
    public class FluentValidationResult : IFluentValidationResult
    {
        private Dictionary<string, List<string>> _errors;

        public bool IsValid => !_errors.Any();

        public IDictionary<string, List<string>> Errors => _errors;

        public FluentValidationResult()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public void AddModelError(IEnumerable<ValidationFailure> failures)
        {
            if (failures == null) return;

            failures.ToList().ForEach(failure => AddModelError(failure.PropertyName, failure.ErrorMessage));
        }

        public void AddModelError(string errorMessage)
        {
            AddModelError("", errorMessage);
        }

        public void AddModelError(string key, string errorMessage)
        {
            if (_errors.ContainsKey(key)) _errors[key].Add(errorMessage);
            else _errors.Add(key, new List<string> { errorMessage });
        }
    }
}
