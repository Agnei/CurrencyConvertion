using Infrastructure.Core.MediatR.Commands;

namespace ConversorMoeda.Application.Commands
{
    public class LiveMoedaCommand : ICommand<CommandResult>
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public decimal Value { get; set; }
    }
}
