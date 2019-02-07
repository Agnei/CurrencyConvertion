using System;
using DryIoc.Microsoft.DependencyInjection;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Core.IoC.DryIoC
{
    public static class WebApiConfiguration
    {
        public static IServiceProvider AddDryIoC<TCompositionRoot>(this IServiceCollection services) where TCompositionRoot : CompositionRoot
        {
            return new Container(rules => rules.WithoutThrowIfDependencyHasShorterReuseLifespan())
            .WithDependencyInjectionAdapter(services, throwIfUnresolved: type => type.Name.EndsWith("Controller"))
            .ConfigureServiceProvider<TCompositionRoot>();
        }

        public static void RegisterInWebRequest<T>(this IContainer container) where T: class
        {
            //version 3.02 of DryIoC make depreceated Reuse.InWebRequest
            container.Register<T>(ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        }
    }
}
