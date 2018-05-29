namespace Agency.ConsoleClient.Tests
{
    using System;
    using BLL.DTOs;
    using BLL.Services;
    using Moq;
    using NUnit.Framework;
    using Services;
    using Services.Commands;

    [TestFixture]
    public class TestCommandUpdateApartment
    {
        [Test]
        public void TestExecute_PreviousCommandReturnsNull_ArgumentNullException()
        {
            // arrange
            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();
            mockPreviousCommand.Setup(x => x.Execute()).Returns((ApartmentEditDto)null);
            var command = new CommandUpdateAparment(new Mock<IConsoleService>().Object, new Mock<IApartmentService>().Object, mockPreviousCommand.Object);

            // act
            // assert
            Assert.Throws(typeof(ArgumentNullException), () => { command.Execute(); });
        }

        [Test]
        public void TestExecute_PreviousCommandReturnsApartment_CallsUpdateOnApartmentService()
        {
            // arrange
            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.Update(It.IsAny<ApartmentEditDto>()));

            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();
            mockPreviousCommand.Setup(x => x.Execute()).Returns(new ApartmentEditDto());

            var command = new CommandUpdateAparment(
                mockConsoleService.Object,
                mockApartmentService.Object,
                mockPreviousCommand.Object);

            // act
            ApartmentEditDto result = command.Execute();

            // assert
            Assert.IsNotNull(result);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Once);
            mockApartmentService.Verify(x => x.Update(It.IsAny<ApartmentEditDto>()), Times.Once);
        }

    }
}
