using System.Threading.Tasks;
using ConversorMoeda.Application.Commands;
using ConversorMoeda.Application.Interfaces;
using Infrastructure.Core.MediatR.Commands;
using Infrastructure.Core.Validators;
using Infrastructure.Core.WebAPI;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMoeda.WebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConverterMoedaController : ApiControllerBase
    {

        private readonly ICurrencyLayerService _currencyLayerService;

        public ConverterMoedaController(IMediator mediator, 
                                        IFluentValidationResult validationResult,
                                        ICurrencyLayerService currencyLayerService) :
        base(mediator, validationResult) => _currencyLayerService = currencyLayerService;
        
        [HttpGet]
        [Route("listar")]
        [ProducesResponseType(typeof(CommandResult), 200)]
        [ProducesResponseType(typeof(CommandResult), 400)]
        public async Task<ActionResult<CommandResult>> Listar()
            => await _currencyLayerService.ListarMoedas();

        [HttpPut]
        [Route("converter")]
        [ProducesResponseType(typeof(CommandResult), 200)]
        [ProducesResponseType(typeof(CommandResult), 400)]
        public async Task<ActionResult<CommandResult>> Converter(LiveMoedaCommand command)
            => await ExecuteCommand(command);

    }
}