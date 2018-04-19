namespace Agency.ConsoleClient.Tests
{
    using Agency.BLL.DTOs;
    using Moq;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class TestAgencyWorkflowService
    {
        [Test]
        public void TestStart_PermissionFalse_Returns()
        {
            // arrange
            const string exitMessage = "Press any key to exit";

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(exitMessage));
            mockConsoleService.Setup(x => x.ReadKey());
            mockConsoleService.Setup(x => x.GetBool("Are you willing to proceed? (y/n)", true)).Returns(false);

            var mockApartmentWorkflowService = new Mock<IApartmentWorkflowService>();

            var agencyTerminalService = new AgencyWorkflowService(mockConsoleService.Object, mockApartmentWorkflowService.Object);

            // act
            agencyTerminalService.StartEditLoop();

            // assert
            mockConsoleService.Verify(x => x.GetBool("Are you willing to proceed? (y/n)", true), Times.Once);
            mockApartmentWorkflowService.Verify(x => x.EditApartment(), Times.Never);

            mockConsoleService.Verify(x => x.Print(exitMessage), Times.Once);
            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        }


        [Test]
        public void TestStart_PermissionTrue_CallsApartmentService()
        {
            // arrange
            var toggle = false;
            const string exitMessage = "Press any key to exit";

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.Print(exitMessage));
            mockConsoleService.Setup(x => x.ReadKey());
            mockConsoleService.Setup(x => x.GetBool("Are you willing to proceed? (y/n)", true))
                .Returns(() =>
                {
                    toggle = !toggle;
                    return toggle;
                }
            );

            var mockApartmentWorkflowService = new Mock<IApartmentWorkflowService>();

            var agencyTerminalService = new AgencyWorkflowService(mockConsoleService.Object, mockApartmentWorkflowService.Object);

            // act
            agencyTerminalService.StartEditLoop();

            // assert
            mockApartmentWorkflowService.Verify(x => x.EditApartment(), Times.Once);

            mockConsoleService.Verify(x => x.GetBool("Are you willing to proceed? (y/n)", true), Times.Exactly(2));
            mockConsoleService.Verify(x => x.Print(exitMessage), Times.Once);
            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        }

        //[Test]
        //public void TestStart_GetPermissionToProceedReturnsFalse_Returns()
        //{
        //    // arrange
        //    const string exitMessage = "Press any key to exit";

        //    var mockConsoleService = new Mock<IConsoleService>();
        //    mockConsoleService.Setup(x => x.Print(exitMessage));
        //    mockConsoleService.Setup(x => x.ReadKey());

        //    var mockAgencyControllerService = new Mock<IAgencyControllerService>();

        //    mockAgencyControllerService.Setup(x => x.GetPermissionToProceed()).Returns(false);

        //    mockAgencyControllerService.Setup(x => x.PickApartmentForEdit()).Returns((ApartmentEditDto)null);

        //    var agencyTerminalService = new AgencyTerminalService(mockConsoleService.Object, mockAgencyControllerService.Object);

        //    // act
        //    agencyTerminalService.Start();

        //    // assert
        //    mockAgencyControllerService.Verify(x => x.GetPermissionToProceed(), Times.Once);
        //    mockAgencyControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Never);

        //    mockConsoleService.Verify(x => x.Print(exitMessage), Times.Once);
        //    mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        //}

        //[Test]
        //public void TestStart_PickNullApartment()
        //{
        //    // arrange
        //    var toggle = false;
        //    const string exitMessage = "Press any key to exit";

        //    var mockConsoleService = new Mock<IConsoleService>();
        //    mockConsoleService.Setup(x => x.Print(exitMessage));
        //    mockConsoleService.Setup(x => x.ReadKey());

        //    var mockAgencyControllerService = new Mock<IAgencyControllerService>();

        //    mockAgencyControllerService.Setup(x => x.GetPermissionToProceed()).Returns(() =>
        //    {
        //        toggle = !toggle;
        //        return toggle;
        //    });

        //    mockAgencyControllerService.Setup(x => x.PickApartmentForEdit()).Returns((ApartmentEditDto)null);

        //    var agencyTerminalService = new AgencyTerminalService(mockConsoleService.Object, mockAgencyControllerService.Object);

        //    // act
        //    agencyTerminalService.Start();

        //    // assert
        //    mockAgencyControllerService.Verify(x => x.GetPermissionToProceed(), Times.Exactly(2));
        //    mockAgencyControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Never);

        //    mockConsoleService.Verify(x => x.Print(exitMessage), Times.Once);
        //    mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        //}



        //public void TestStart()
        //{
        //    // arrange
        //    var toggle = false;
        //    var exitMessage = "Press any key to exit";

        //    var mockConsoleService = new Mock<IConsoleService>();
        //    mockConsoleService.Setup(x => x.Print(exitMessage));
        //    mockConsoleService.Setup(x => x.ReadKey());

        //    var mockAgencyControllerService = new Mock<IAgencyControllerService>();

        //    mockAgencyControllerService.Setup(x => x.GetPermissionToProceed()).Returns(() =>
        //    {
        //        toggle = !toggle;
        //        return toggle;
        //    });

        //    mockAgencyControllerService.Setup(x => x.PickApartmentForEdit()).Returns((ApartmentEditDto)null);

        //    var agencyTerminalService = new AgencyTerminalService(mockConsoleService.Object, mockAgencyControllerService.Object);

        //    // act
        //    agencyTerminalService.Start();

        //    // assert
        //    mockAgencyControllerService.Verify(x => x.GetPermissionToProceed(), Times.Exactly(2));

        //    mockAgencyControllerService.Verify(x => x.UpdateApartment(It.IsAny<ApartmentEditDto>()), Times.Never);

        //    mockConsoleService.Verify(x => x.Print(exitMessage), Times.Once);

        //    mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        //}
    }
}
