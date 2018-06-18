namespace Agency.ConsoleClient.Tests
{
    using Agency.ConsoleClient.Services.Commands;
    using BLL.DTOs;
    using Moq;
    using NUnit.Framework;
    using Services;
    using Services.Factories;
    using System;
    using NLog;

    [TestFixture]
    public class TestAgencyWorkflowService
    {
        [Test]
        public void TestStart_PermissionFalse_Returns()
        {
            // arrange
            var outputText = "Are you willing to proceed? (y/n)";
            var mockCommandFactory = new Mock<ICommandFactory<ApartmentEditDto>>();

            var mockLogger = new Mock<ILogger>();

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.GetBool(outputText)).Returns(false);

            var agencyWorkflowService = new AgencyWorkflowService(mockCommandFactory.Object, mockConsoleService.Object, mockLogger.Object);

            // act
            agencyWorkflowService.Start();

            // assert
            mockConsoleService.Verify(x => x.GetBool(outputText), Times.Once);
            mockConsoleService.Verify(x => x.Clear(), Times.Never);
            mockCommandFactory.Verify(x => x.ChainCommands(), Times.Never);
            mockConsoleService.Verify(x => x.Print("Press any key to exit"), Times.Once);
            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        }

        [Test]
        public void TestStart_CommandThrowsException_PrintsException()
        {
            // arrange
            var flag = false;

            var outputText = "Are you willing to proceed? (y/n)";

            var mockLogger = new Mock<ILogger>();

            var mockCommand = new Mock<BaseCommand<ApartmentEditDto>>(mockLogger.Object);
            mockCommand.Setup(x => x.Execute()).Throws<Exception>();

            var mockCommandFactory = new Mock<ICommandFactory<ApartmentEditDto>>();
            mockCommandFactory.Setup(x => x.ChainCommands()).Returns(mockCommand.Object);

            var mockConsoleService = new Mock<IConsoleService>();
            mockConsoleService.Setup(x => x.GetBool(outputText)).Returns(() =>
            {
                flag = !flag;
                return flag;
            });

            var agencyWorkflowService = new AgencyWorkflowService(mockCommandFactory.Object, mockConsoleService.Object, mockLogger.Object);

            // act
            agencyWorkflowService.Start();

            // assert
            mockConsoleService.Verify(x => x.GetBool(outputText), Times.Exactly(2));
            mockConsoleService.Verify(x => x.Clear(), Times.Once);
            mockCommandFactory.Verify(x => x.ChainCommands(), Times.Once);
            mockCommand.Verify(x => x.Execute(), Times.Once);
            mockConsoleService.Verify(x => x.Print(It.IsAny<Exception>()), Times.Once);
            mockConsoleService.Verify(x => x.Print("Press any key to exit"), Times.Once);
            mockConsoleService.Verify(x => x.ReadKey(), Times.Once);
        }
    }
}
