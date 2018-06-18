namespace Agency.ConsoleClient.Tests
{
    using System;
    using BLL.DTOs;
    using BLL.Services;
    using DAL.Model.Entities;
    using Moq;
    using NLog;
    using NUnit.Framework;
    using Services;
    using Services.Commands;

    [TestFixture]
    public class TestCommandUpdateApartment
    {
        [Test]
        public void TestExecute_PreviousCommandReturnsNull_Null()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();

            var mockConsoleService = new Mock<IConsoleService>();

            var mockApartmentService = new Mock<IApartmentService>();

            var mockPreviousCommand = new Mock<BaseCommand<ApartmentEditDto>>(mockLogger.Object);
            mockPreviousCommand.Setup(x => x.Execute()).Returns((ApartmentEditDto)null);

            var command = new CommandUpdateAparment(
                mockConsoleService.Object,
                mockApartmentService.Object,
                mockPreviousCommand.Object,
                () => mockLogger.Object);

            // act
            ApartmentEditDto result = command.Execute();

            // assert
            Assert.IsNull(result);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Once);
            mockConsoleService.Verify(x => x.Print("Nothing to update"), Times.Once);
            mockApartmentService.Verify(x => x.Update(It.IsAny<ApartmentEditDto>()), Times.Never);
        }

        [Test]
        public void TestExecute_PreviousCommandReturnsApartment_CallsUpdateOnApartmentService()
        {
            // arrange
            var mockLogger = new Mock<ILogger>();

            string outputText = $"Apartment with id: {0} is successfully updated with status: {ApartmentState.PartitionsDesigning:G}";

            var mockConsoleService = new Mock<IConsoleService>();

            var mockApartmentService = new Mock<IApartmentService>();

            var mockPreviousCommand = new Mock<BaseCommand<ApartmentEditDto>>(mockLogger.Object);
            mockPreviousCommand.Setup(x => x.Execute()).Returns(new ApartmentEditDto());

            var command = new CommandUpdateAparment(
                mockConsoleService.Object,
                mockApartmentService.Object,
                mockPreviousCommand.Object,
                () => mockLogger.Object);

            // act
            ApartmentEditDto result = command.Execute();

            // assert
            Assert.IsNotNull(result);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Once);
            mockConsoleService.Verify(x => x.Print(outputText), Times.Once);
            mockApartmentService.Verify(x => x.Update(It.IsAny<ApartmentEditDto>()), Times.Once);
        }

    }
}
