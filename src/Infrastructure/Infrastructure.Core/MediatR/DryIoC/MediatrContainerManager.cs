using DryIoc;
using MediatR;

namespace Infrastructure.Core.MediatR.DryIoC
{
    public static class MediatrContainerConfiguration
    {
        public static IMediator ConfigureMediatr(this IContainer container)
        {
            container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
            container.RegisterMany(new[] { typeof(IMediator).GetAssembly() }, Registrator.Interfaces);
            return container.Resolve<IMediator>();
        }
    }
}
