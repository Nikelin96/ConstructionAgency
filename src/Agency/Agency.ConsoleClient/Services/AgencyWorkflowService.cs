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
            while (_consoleService.GetBool("Are you willing to proceed? (y/n)", true))
            {

                _apartmentWorkflowService.EditApartment();
                //ApartmentEditDto selectedApartment = _apartmentControllerService.PickApartmentForEdit();

                //if (selectedApartment == null)
                //{
                //    return;
                //}

                //// todo replace with navigation in the application
                ////_consoleService.Print("Press any key to continue");
                ////_consoleService.ReadKey();
                ////_consoleService.Print();

                //ApartmentEditDto updatedApartment = _apartmentControllerService.UpdateApartment(selectedApartment);

                //if (updatedApartment == null)
                //{
                //    _consoleService.Print($"Failed to update apartment {selectedApartment.Id}");
                //    return;
                //}

                //_consoleService.ReadKey();
            }

            _consoleService.Print("Press any key to exit");
            _consoleService.ReadKey();
        }
    }
}