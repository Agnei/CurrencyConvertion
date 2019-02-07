using System.Reflection;
using DryIoc;
using MediatR;
using Infrastructure.Core.Validators;
using Infrastructure.Core.IoC.DryIoC;
using Infrastructure.Core.MediatR.DryIoC;
using Infrastructure.Core.MediatR.Validators;
using Infrastructure.Core.Http;
using ConversorMoeda.Application.CommandHandlers;
using ConversorMoeda.CurrencyLayer.Services;
using ConversorMoeda.Application.Commands;

namespace ConversorMoeda.WebApi.Configuration
{
    public class ConversorMoedaCompositeRoot : CompositionRoot
    {
        public ConversorMoedaCompositeRoot(IContainer r) : base(r)
        {
            r.RegisterMany(new Assembly[]{
                typeof(ConverterMoedaCommandHandler).GetAssembly(),
                typeof(LiveMoedaCommand).GetAssembly(),
                typeof(CurrencyLayerService).GetAssembly()
                }, Registrator.Interfaces, reuse: Reuse.Singleton);

            r.Register<IFluentValidationResult, FluentValidationResult>(reuse: Reuse.Scoped, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
            r.Register(typeof(IPipelineBehavior<,>), typeof(ValidatorsBehavior<,>), reuse: Reuse.Scoped, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
            r.Register<ICommandHttpClient, CommandHttpClient>(reuse: Reuse.Singleton);

            r.ConfigureMediatr();
        }
    }
}