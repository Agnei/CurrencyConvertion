using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Core.MediatR.Commands;
using ConversorMoeda.Application.Interfaces;
using ConversorMoeda.Application.Commands;
using ConversorMoeda.Application.Entities;

namespace ConversorMoeda.Application.CommandHandlers
{
    public class ConverterMoedaCommandHandler : IRequestHandler<LiveMoedaCommand, CommandResult>
    {
        private readonly ICurrencyLayerService _currencyLayerService;
        
        public ConverterMoedaCommandHandler(ICurrencyLayerService currencyLayerService)
        {
            _currencyLayerService = currencyLayerService;
        }

        public async Task<CommandResult> Handle(LiveMoedaCommand request, CancellationToken cancellationToken)
        {
            var _currencyResult = await _currencyLayerService.Execute(request, cancellationToken);

            var _response = ((CurrencyConverterResult)_currencyResult.Data);

            var result = new CommandResult();

            if (_currencyResult.Valid && (_response?.Success ?? false))
                result = CommandResult
                                .Success(new ConverterMoedaModelView(request.Source, 
                                                                     request.Target, 
                                                                     _response.Quotes[string.Concat(request.Source, request.Target)], 
                                                                     request.Value));
            else
                result = CommandResult.Failed(string.Empty, "Failed to convert amount to quotes requested");

            return await Task.FromResult(result);
        }
    }
}
