using System.Threading;
using System.Threading.Tasks;
using ConversorMoeda.Application.Commands;
using ConversorMoeda.Application.Entities;
using Infrastructure.Core.MediatR.Commands;

namespace ConversorMoeda.Application.Interfaces
{
    public interface ICurrencyLayerService
    {
        ValueTask<CommandResult<CurrencyConverterResult>> Execute(LiveMoedaCommand command,
                                                                  CancellationToken cancellationToken);

        ValueTask<CommandResult<ListaMoedaModelView>> ListarMoedas();
    }
}