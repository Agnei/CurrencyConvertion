using FluentValidation;
using ConversorMoeda.Application.Commands;

namespace ConversorMoeda.Application.Validators
{
    public class ConverterMoedaCommandValidator: AbstractValidator<LiveMoedaCommand>
    {
        public ConverterMoedaCommandValidator()
        {
            RuleFor(x => x).Custom((model, context) =>
            {
                if (model.Source == null || model.Target == null)
                    context.AddFailure("Inform Source and Target to convert");
            });

            RuleFor(x => x.Value)
                    .NotEqual(0)
                    .WithMessage("Value shoud not be zero!");
        }
    }
}
