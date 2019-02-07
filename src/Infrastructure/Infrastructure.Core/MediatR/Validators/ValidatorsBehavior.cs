using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Core.Validators;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.MediatR.Validators
{
    public class ValidatorsBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly IFluentValidationResult _validationResult;

        public ValidatorsBehavior(IValidator<TRequest> validator,
                                  IFluentValidationResult validationResult)
        {
            _validator = validator;
            _validationResult = validationResult;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validator == null)
                return await next();

            var result = _validator.Validate(request);

            return result.IsValid ? await next() : await Errors(result.Errors);
        }

        private Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            _validationResult.AddModelError(failures);
            return Task.FromResult(default(TResponse));
        }
    }
}
