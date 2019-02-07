using MediatR;

namespace Infrastructure.Core.MediatR.Commands
{
    public interface ICommand<out T> : IRequest<T>
    {
    }

    public interface ICommand : ICommand<bool>
    {
    }
}
