using ConversorMoeda.Application.CommandHandlers;
using ConversorMoeda.Application.Commands;
using ConversorMoeda.Application.Entities;
using ConversorMoeda.Application.Interfaces;
using Infrastructure.Core.MediatR.Commands;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test.Unit.ApplicationTest
{
    public class CommandHandler_Test
    {
        [Fact]
        public void ConverterMoeadaCommandHandler_WhenPass_ShouldReturnCurrencyResult()
        {
            //Mock
            var _mockHandler = new Mock<IRequestHandler<LiveMoedaCommand, CommandResult>>();
            var _mockServiceCurrency = new Mock<ICurrencyLayerService>();


            //Fake
            var _liveCommand = new LiveMoedaCommand() { Source = "USD", Target = "BRL", Value = 10 };
            var resultFake = new ConverterMoedaModelView("USD", "BRL", decimal.Parse("0.33434"), 10);
            var resulServiceFake = new CurrencyConverterResult()
            {
                Success = true,
                Quotes = new Dictionary<string, decimal>
                {
                    { "USDBRL", decimal.Parse("0.33434") }
                }
            };

            //Arrange
            _mockHandler.Setup(moq => moq.Handle(_liveCommand, default(CancellationToken)))
                        .Returns(Task.FromResult(CommandResult.Success(resultFake)));

            _mockServiceCurrency.Setup(moq => moq.Execute(_liveCommand, default(CancellationToken)))
                   .Returns(new ValueTask<CommandResult<CurrencyConverterResult>>(Task.FromResult(CommandResult<CurrencyConverterResult>.Success(resulServiceFake))));

            var _commandHandler = new ConverterMoedaCommandHandler(_mockServiceCurrency.Object);

            //Act
            var fakeResult = _mockHandler.Object.Handle(_liveCommand, default(CancellationToken));
            var response = _commandHandler.Handle(_liveCommand, default(CancellationToken));

            //Assert
            Assert.Equal(((ConverterMoedaModelView)response.Result.Data).Result, ((ConverterMoedaModelView)fakeResult.Result.Data).Result);
        }
    }
}
