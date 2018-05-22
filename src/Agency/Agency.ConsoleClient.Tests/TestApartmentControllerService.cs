namespace Agency.ConsoleClient.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BLL.DTOs;
    using BLL.Services;
    using DAL.Model.Entities;
    using Moq;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class TestApartmentControllerService
    {
        [Test]
        public void TestPickApartmentForEdit_NegativeNumber_Null()
        {
            // arrange
            var apartmentDtos = new List<ApartmentEditDto>() { new ApartmentEditDto { Id = It.IsAny<int>() } };

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()));

            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(-1);

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.GetAll(null)).Returns(apartmentDtos);

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, mockApartmentService.Object, null);

            // act
            ApartmentEditDto result = apartmentControllerService.PickApartmentForEdit();


            // assert
            mockApartmentService.Verify(x => x.GetAll(null), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(3));
            mockConsoleService.Verify(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()), Times.Once);
            mockConsoleService.Verify(x => x.Print("List of appartments: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Select Apartment: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Invalid number entered: only positive numbers and 0 are allowed"), Times.Once);
            mockConsoleService.Verify(x => x.Print("Element with such Index number does not exist"), Times.Never);

            Assert.IsNull(result);
        }


        [Test]
        public void TestPickApartmentForEdit_NonExistingNumber_Null()
        {
            // arrange
            var apartmentDtos = new List<ApartmentEditDto>() { new ApartmentEditDto { Id = It.IsAny<int>() } };

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()));

            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(0);

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.GetAll(null)).Returns(apartmentDtos);

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, mockApartmentService.Object, null);

            // act
            ApartmentEditDto result = apartmentControllerService.PickApartmentForEdit();

            // assert
            mockApartmentService.Verify(x => x.GetAll(null), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(3));
            mockConsoleService.Verify(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()), Times.Once);
            mockConsoleService.Verify(x => x.Print("List of appartments: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Select Apartment: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Invalid number entered: only positive numbers and 0 are allowed"), Times.Never);
            mockConsoleService.Verify(x => x.Print("Element with such Index number does not exist"), Times.Once);

            Assert.IsNull(result);
        }


        [Test]
        public void TestPickApartmentForEdit_ExistingNumber_Result()
        {
            // arrange
            var apartmentDtos = new List<ApartmentEditDto>() { new ApartmentEditDto { Id = It.IsAny<int>() } };

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()));

            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(1);

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.GetAll(null)).Returns(apartmentDtos);

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, mockApartmentService.Object, null);

            // act
            ApartmentEditDto result = apartmentControllerService.PickApartmentForEdit();

            // assert

            mockApartmentService.Verify(x => x.GetAll(null), Times.Once);

            mockConsoleService.Verify(x => x.Print(It.IsAny<string>()), Times.Exactly(2));
            mockConsoleService.Verify(x => x.Print(It.IsAny<IDictionary<int, ApartmentEditDto>>()), Times.Once);
            mockConsoleService.Verify(x => x.Print("List of appartments: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Select Apartment: "), Times.Once);
            mockConsoleService.Verify(x => x.Print("Invalid number entered: only positive numbers and 0 are allowed"), Times.Never);
            mockConsoleService.Verify(x => x.Print("Element with such Index number does not exist"), Times.Never);

            Assert.AreEqual(apartmentDtos.Single(), result);
        }

        [Test]
        public void TestUpdateApartment_Null_Null()
        {
            // arrange
            var outputText = "Apartment is null";
            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(outputText));

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, null, null);

            // act
            ApartmentEditDto result = apartmentControllerService.UpdateApartment(null);

            // assert
            mockConsoleService.Verify(x => x.Print(outputText), Times.Once);

            Assert.IsNull(result);
        }

        [Test]
        public void TestUpdateApartment_NotAllowedState_Null()
        {
            // arrange 
            var finalState = ApartmentState.Done;
            var dto = new ApartmentEditDto { Id = It.IsAny<int>(), State = finalState, Name = "ap1" };
            string outputText =
                $"Apartment {dto.Id}, {dto.Name} is in final state {dto.State:G}";

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(outputText));

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(finalState))
                .Returns(Enumerable.Empty<ApartmentState>().ToList());

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, null, mockApartmentStateService.Object);

            // act
            ApartmentEditDto result = apartmentControllerService.UpdateApartment(dto);

            // assert
            mockConsoleService.Verify(x => x.Print(outputText), Times.Once);

            Assert.IsNull(result);
        }

        [Test]
        public void TestUpdateApartment_NegativeNumber_Null()
        {
            // arrange
            var state = ApartmentState.PartitionsDesigning;
            var dto = new ApartmentEditDto { Id = It.IsAny<int>(), State = state, Name = "ap1" };
            IList<ApartmentState> allowedStates = new ApartmentStateService().GetAllowedApartmentStates(state);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(allowedStates));
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns(-1);

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(state)).Returns(allowedStates);

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, null, mockApartmentStateService.Object);

            // act
            ApartmentEditDto result = apartmentControllerService.UpdateApartment(dto);

            // assert
            mockConsoleService.Verify(x => x.Print("Set new Apartment Status:"), Times.Once);
            mockConsoleService.Verify(x => x.Print((string)null), Times.Once);
            mockConsoleService.Verify(x => x.Print(allowedStates), Times.Once);
            mockConsoleService.Verify(x => x.Print("Invalid number entered: only positive numbers and 0 are allowed"), Times.Once);

            Assert.IsNull(result);
        }

        [Test]
        public void TestUpdateApartment_InvalidNumber_Null()
        {
            // arrange
            var startState = ApartmentState.PartitionsDesigning;
            var newState = ApartmentState.PartitionsDesigning;
            var dto = new ApartmentEditDto { Id = It.IsAny<int>(), State = startState, Name = "ap1" };
            IList<ApartmentState> allowedStates = new ApartmentStateService().GetAllowedApartmentStates(startState);

            string message =
                "Allowed States are:" +
                Environment.NewLine +
                string.Join(
                    Environment.NewLine,
                    allowedStates.Select(state => $"{(int)state} {state:G}").ToArray()
                );

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(allowedStates));
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns((int)newState);

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(startState)).Returns(allowedStates);
            mockApartmentStateService.Setup(x => x.Validate(dto, newState)).Returns((isValid: false, message: message));

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, null, mockApartmentStateService.Object);

            // act
            ApartmentEditDto result = apartmentControllerService.UpdateApartment(dto);

            // assert
            mockConsoleService.Verify(x => x.Print("Set new Apartment Status:"), Times.Once);
            mockConsoleService.Verify(x => x.Print(allowedStates), Times.Once);
            mockConsoleService.Verify(x => x.Print((string)null), Times.Once);
            mockConsoleService.Verify(x => x.Print($"Cannot switch to the state: {newState}"));
            mockConsoleService.Verify(x => x.Print(message), Times.Once);

            Assert.IsNull(result);
        }

        [Test]
        public void TestUpdateApartment_ValidNumber_UpdatedApartment()
        {
            // arrange
            var startState = ApartmentState.PartitionsDesigning;
            var newState = ApartmentState.DrainageInstallation;
            var dto = new ApartmentEditDto { Id = It.IsAny<int>(), State = startState, Name = "ap1" };
            IList<ApartmentState> allowedStates = new ApartmentStateService().GetAllowedApartmentStates(startState);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(allowedStates));
            mockConsoleService.Setup(x => x.Print(It.IsAny<string>()));
            mockConsoleService.Setup(x => x.GetInputAsNonNegativeNumber()).Returns((int)newState);

            var mockApartmentStateService = new Mock<IApartmentStateService>();
            mockApartmentStateService.Setup(x => x.GetAllowedApartmentStates(startState)).Returns(allowedStates);
            mockApartmentStateService.Setup(x => x.Validate(dto, newState)).Returns((isValid: true, message: string.Empty));

            var mockApartmentService = new Mock<IApartmentService>();
            mockApartmentService.Setup(x => x.Update(dto));

            var apartmentControllerService = new ApartmentControllerService(mockConsoleService.Object, mockApartmentService.Object, mockApartmentStateService.Object);

            // act
            ApartmentEditDto result = apartmentControllerService.UpdateApartment(dto);

            // assert
            mockConsoleService.Verify(x => x.Print("Set new Apartment Status:"), Times.Once);
            mockConsoleService.Verify(x => x.Print(allowedStates), Times.Once);
            mockConsoleService.Verify(x => x.Print((string)null), Times.Once);
            mockConsoleService.Verify(x => x.Print($"Apartment with id: {dto.Id} is successfully updated with status: {newState:G}"), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.State, newState);
            });
        }
    }
}
