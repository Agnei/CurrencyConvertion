using Infrastructure.Core.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Infrastructure.Core.MediatR.Commands;
using ConversorMoeda.Application.Entities;
using ConversorMoeda.Application.Commands;
using ConversorMoeda.Application.Interfaces;
using System.Threading;

namespace ConversorMoeda.CurrencyLayer.Services
{
    public class CurrencyLayerService : ICurrencyLayerService
    {
        //****
        //https://currencylayer.com/documentation
        //****
        private const string LiveENDPOINT = "http://apilayer.net/api/live?access_key={0}&source={1}";
        private const string ListaENDPOINT = "http://apilayer.net/api/list?access_key={0}";

        private string _keyAccess;

        private readonly ICommandHttpClient _httpClient;
        
        public CurrencyLayerService(ICommandHttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _keyAccess = configuration["CurrencyLayer:AccessKey"];
            
            if (_keyAccess == null)
                throw new ArgumentNullException("Access Key for apilayer should not be null!");
        }

        public async ValueTask<CommandResult<CurrencyConverterResult>> Execute(LiveMoedaCommand command, CancellationToken cancellationToken)
        {
            var _endpoint = string.Format(LiveENDPOINT, _keyAccess, command.Source);

            return await _httpClient
                        .GetStringAsync<CurrencyConverterResult>(uri: _endpoint, 
                                                                 cancellationToken: cancellationToken);
        }


        public async ValueTask<CommandResult<ListaMoedaModelView>> ListarMoedas()
        {
            var _endpoit = string.Format(ListaENDPOINT, _keyAccess);

            return await _httpClient
                        .GetStringAsync<ListaMoedaModelView>(uri: _endpoit,
                                                                 cancellationToken: default(CancellationToken));
        }

    }
}
