namespace Agency.ConsoleClient.Tests
{
    using BLL.DTOs;
    using Moq;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class TestApartmentWorkflowService
    {
        [Test]
        public void TestEditApartment_Null_Returns()
        {
            // arrange
            var mockApartmentControllerService = new Mock<IApartmentControllerService>();
            mockApartmentControllerService.Setup(x => x.PickApartmentForEdit()).Returns((ApartmentEditDto)null);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.ReadKey());

            var apartmentWorkflowService = new ApartmentWorkflowService(mockConsoleService.Object, mockApartmentControllerService.Object);

            // act
            apartmentWorkflowService.EditApartment();

            // assert
            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
            mockApartmentControllerService.Verify(x => x.PickApartmentForEdit(), Times.Once);
            mockApartmentControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Never);
        }


        [Test]
        public void TestEditApartment_NotNull_ControllerReturnsNull_Returns()
        {
            var apartmentToUpdate = new ApartmentEditDto { Id = 0 };
            string outputMessage = $"Failed to update apartment {apartmentToUpdate.Id}";

            var mockApartmentControllerService = new Mock<IApartmentControllerService>();
            mockApartmentControllerService.Setup(x => x.PickApartmentForEdit()).Returns(apartmentToUpdate);
            mockApartmentControllerService.Setup(x => x.UpdateApartment(apartmentToUpdate)).Returns((ApartmentEditDto)null);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(outputMessage));

            var apartmentWorkflowService = new ApartmentWorkflowService(mockConsoleService.Object, mockApartmentControllerService.Object);

            apartmentWorkflowService.EditApartment();

            mockApartmentControllerService.Verify(x => x.PickApartmentForEdit(), Times.Once);
            mockApartmentControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Once);
            mockConsoleService.Verify(x => x.Print(outputMessage), Times.Once);
        }

        [Test]
        public void TestEditApartment_NotNull_ControllerReturns_Returns()
        {
            var apartmentToUpdate = new ApartmentEditDto { Id = 0 };

            var mockApartmentControllerService = new Mock<IApartmentControllerService>();
            mockApartmentControllerService.Setup(x => x.PickApartmentForEdit()).Returns(apartmentToUpdate);
            mockApartmentControllerService.Setup(x => x.UpdateApartment(apartmentToUpdate)).Returns(apartmentToUpdate);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.ReadKey());

            var apartmentWorkflowService = new ApartmentWorkflowService(mockConsoleService.Object, mockApartmentControllerService.Object);

            apartmentWorkflowService.EditApartment();

            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
            mockApartmentControllerService.Verify(x => x.PickApartmentForEdit(), Times.Once);
            mockApartmentControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Once);
        }

    }
}
