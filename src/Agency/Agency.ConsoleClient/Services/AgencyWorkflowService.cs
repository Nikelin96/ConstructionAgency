namespace Agency.ConsoleClient.Services
{
    using System;
    using BLL.DTOs;

    public class AgencyWorkflowService : IAgencyWorkflowService
    {
        private readonly IConsoleService _consoleService;

        private readonly IApartmentWorkflowService _apartmentWorkflowService;

        public AgencyWorkflowService(IConsoleService consoleService, IApartmentWorkflowService apartmentWorkflowService)
        {
            _consoleService = consoleService;
            _apartmentWorkflowService = apartmentWorkflowService;
        }

        public void StartEditLoop()
        {
            while (_consoleService.GetBool("Are you willing to proceed? (y/n)"))
            {
                _consoleService.Clear();

                _apartmentWorkflowService.EditApartment();
            }

            _consoleService.Print("Press any key to exit");
            _consoleService.ReadKey();
        }
    }
}