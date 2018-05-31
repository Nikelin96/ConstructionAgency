namespace Agency.ConsoleClient.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using BLL.DTOs;
    using BLL.Services;
    using DAL.Model.Entities;
    using Moq;
    using NUnit.Framework;
    using Services;
    using Services.Commands;

    [TestFixture]
    public class TestCommandGetModifiedApartment
    {
        [Test]
        public void TestExecute_PreviousCommandReturnsNull_ArgumentNullException()
        {
            // arrange
            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();
            mockPreviousCommand.Setup(x => x.Execute()).Returns((ApartmentEditDto)null);

            var command = new CommandGetModifiedApartment(new Mock<IConsoleService>().Object, new Mock<IApartmentStateService>().Object, mockPreviousCommand.Object);

            // act
            // assert
            Assert.Throws(typeof(ArgumentNullException), () => { command.Execute(); });
        }

        [Test]
        public void TestExecute_NoAllowedStates_UnmodifiedApartment()
        {
            // arrange
            var dto = new ApartmentEditDto { Id = 0 };

            string outputText =
                $"Apartment {dto.Id}, {dto.Name} is in final state {dto.State:G}";

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(It.IsAny<ApartmentState>())).Returns(Enumerable.Empty<ApartmentState>());

            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();

            mockPreviousCommand.Setup(x => x.Execute()).Returns(dto);

            var command = new CommandGetModifiedApartment(mockConsoleService.Object, mockApartmentStateService.Object, mockPreviousCommand.Object);

            // act
            ApartmentEditDto result = command.Execute();

            // assert
            Assert.AreEqual(result, dto);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Once);
            mockConsoleService.Verify(x => x.Print(outputText), Times.Once);
        }

        [Test]
        public void TestExecute_InvalidState_ValidationException()
        {
            // arrange
            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.Print(It.IsAny<IEnumerable<ApartmentState>>()));
            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(It.IsAny<int>());

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(It.IsAny<ApartmentState>())).Returns(new List<ApartmentState> { ApartmentState.PartitionsDesigning });
            mockApartmentStateService.Setup(x => x.Validate(It.IsAny<ApartmentEditDto>(), It.IsAny<ApartmentState>())).Returns(() => (isValid: false, message: string.Empty));

            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();
            mockPreviousCommand.Setup(x => x.Execute()).Returns(new ApartmentEditDto());

            var command = new CommandGetModifiedApartment(mockConsoleService.Object, mockApartmentStateService.Object, mockPreviousCommand.Object);
            // act
            // assert
            Assert.Throws(typeof(ValidationException), () =>
                {
                    command.Execute();
                });

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(3));
            mockConsoleService.Verify(x => x.Print("Set new Apartment Status:"), Times.Once);
            mockConsoleService.Verify(x => x.Print((string)null), Times.Once);
            mockConsoleService.Verify(x => x.Print(string.Empty), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<IEnumerable<ApartmentState>>()), Times.Once);
            mockConsoleService.Verify(x => x.GetInputAsNonNegativeNumber(), Times.Once);
        }

        [Test]
        public void TestExecute_ValidState_ModifiedApartment()
        {
            // arrange
            var state = ApartmentState.PartitionsDesigning;

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.Print(It.IsAny<IEnumerable<ApartmentState>>()));
            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(2);

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(It.IsAny<ApartmentState>())).Returns(new List<ApartmentState> { ApartmentState.PartitionsDesigning });
            mockApartmentStateService.Setup(x => x.Validate(It.IsAny<ApartmentEditDto>(), It.IsAny<ApartmentState>())).Returns(() => (isValid: true, message: string.Empty));

            var mockPreviousCommand = new Mock<ICommand<ApartmentEditDto>>();
            mockPreviousCommand.Setup(x => x.Execute()).Returns(new ApartmentEditDto() { State = state });

            var command = new CommandGetModifiedApartment(mockConsoleService.Object, mockApartmentStateService.Object, mockPreviousCommand.Object);

            // act
            ApartmentEditDto result = command.Execute();

            // assert
            Assert.AreNotEqual(state, result.State);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(2));
            mockConsoleService.Verify(x => x.Print("Set new Apartment Status:"), Times.Once);
            mockConsoleService.Verify(x => x.Print((string)null), Times.Once);
            mockConsoleService.Verify(x => x.Print(It.IsAny<IEnumerable<ApartmentState>>()), Times.Once);
            mockConsoleService.Verify(x => x.GetInputAsNonNegativeNumber(), Times.Once);
        }
    }
}
