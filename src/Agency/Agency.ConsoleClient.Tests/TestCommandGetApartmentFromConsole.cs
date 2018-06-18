namespace Agency.ConsoleClient.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using BLL.DTOs;
    using BLL.Services;
    using Moq;
    using NLog;
    using NUnit.Framework;
    using Services;
    using Services.Commands;

    [TestFixture]
    public class TestCommandGetApartmentFromConsole
    {
        [Test]
        public void TestExecute_ApartmentNotFoundByNumber_Null()
        {
            // arrange
            var mockConsoleService = new Mock<IConsoleService>();

            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(1);

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.GetAll(null)).Returns(Enumerable.Empty<ApartmentEditDto>().ToList());

            var mockLogger = new Mock<ILogger>();

            var command = new CommandGetApartmentFromConsole(mockConsoleService.Object, mockApartmentService.Object, () => mockLogger.Object);

            ApartmentEditDto result = command.Execute();

            // assert
            Assert.IsNull(result);

            mockApartmentService.Verify(x => x.GetAll(null), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(3));
            mockConsoleService.Verify(x => x.Print("List of appartments: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Select Apartment: "), Times.Once);
            mockConsoleService.Verify(x => x.Print($"Element by Serial Number: {1} does not exist"), Times.Once);
            mockConsoleService.Verify(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()), Times.Once);
        }


        [Test]
        public void TestExecute_ApartmentFoundByNumber_FoundApartment()
        {
            // arrange
            var apartmentDto = new ApartmentEditDto();

            var mockConsoleService = new Mock<IConsoleService>();

            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(1);

            var mockApartmentService = new Mock<IApartmentService>();

            mockApartmentService.Setup(x => x.GetAll(null)).Returns(new List<ApartmentEditDto> { apartmentDto });

            var mockLogger = new Mock<ILogger>();

            var command = new CommandGetApartmentFromConsole(mockConsoleService.Object, mockApartmentService.Object, () => mockLogger.Object);

            ApartmentEditDto result = command.Execute();

            // assert
            Assert.AreEqual(result, apartmentDto);

            mockApartmentService.Verify(x => x.GetAll(null), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(2));
            mockConsoleService.Verify(x => x.Print("List of appartments: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Select Apartment: "), Times.Once);
            mockConsoleService.Verify(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()), Times.Once);
        }
    }
}
