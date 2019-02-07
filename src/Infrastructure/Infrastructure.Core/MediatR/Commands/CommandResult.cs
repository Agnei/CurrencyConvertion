using FluentValidation.Results;
using System.Collections.Generic;

namespace Infrastructure.Core.MediatR.Commands
{
    public class CommandResult
    {
        public bool Valid { get; private set; }
        public string Message { get; private set; }
        public virtual object Data { get; private set; }
        public int MyProperty { get; set; }

        public CommandResult()
        {

        }
        public static CommandResult Success(object data, string message = null)
        {
            return new CommandResult(true, message, data);
        }

        protected CommandResult(bool valid, string message, object data)
        {
            Valid = valid;
            Message = message;
            Data = data;
        }

        public static CommandResult Failed(IEnumerable<ValidationFailure> failures)
        {
            return new CommandResult(false, "requisição inválida", failures);
        }

        public static CommandResult Failed(string propertyName, string errorMessage)
        {
            var validationFailure = new ValidationFailure(propertyName, errorMessage);
            return Failed(new ValidationFailure[] { validationFailure });
        }
    }

    public class CommandResult<T> : CommandResult where T : class
    {
        private CommandResult(bool valid, string message, T data) : base(valid, message, data)
        {

        }

        public static CommandResult<T> Success(T data)
        {
            return new CommandResult<T>(true, string.Empty, data);
        }

        public new static CommandResult<T> Failed(string propertyName, string errorMessage)
            => new CommandResult<T>(false, $"{propertyName} - {errorMessage}", default(T));
    }
}