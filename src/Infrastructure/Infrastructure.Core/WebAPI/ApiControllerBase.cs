using Infrastructure.Core.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Infrastructure.Core.MediatR.Commands;

namespace Infrastructure.Core.WebAPI
{
    public class ApiControllerBase : ControllerBase
    {

        private readonly IFluentValidationResult _validationResult;
        private readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator, IFluentValidationResult validationResult)
        {
            _mediator = mediator;
            _validationResult = validationResult;
        }

        [NonAction]
        protected async Task<ActionResult<CommandResult>> ExecuteCommand(ICommand<CommandResult> command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!_validationResult.Errors.Any())
                    return Ok(result);

                return BadRequest(_validationResult.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}