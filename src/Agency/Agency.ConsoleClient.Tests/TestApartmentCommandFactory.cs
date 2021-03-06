﻿namespace Agency.ConsoleClient.Tests
{
    using BLL.DTOs;
    using BLL.Services;
    using Moq;
    using NLog;
    using NUnit.Framework;
    using Services;
    using Services.Commands;
    using Services.Factories;

    [TestFixture]
    public class TestApartmentCommandFactory
    {
        [Test]
        public void TestChainCommands()
        {
            var mockConsoleService = new Mock<IConsoleService>();
            var mockApartmentService = new Mock<IApartmentService>();
            var mockApartmentStateService = new Mock<IApartmentStateService>();

            var mockLogger = new Mock<ILogger>();

            var commandFactory = new ApartmentCommandFactory(mockConsoleService.Object, mockApartmentService.Object, mockApartmentStateService.Object, () => mockLogger.Object);

            BaseCommand<ApartmentEditDto> result = commandFactory.ChainCommands();

            Assert.NotNull(result);
            Assert.AreEqual(result.GetType(), typeof(CommandUpdateAparment));
        }
    }
}
