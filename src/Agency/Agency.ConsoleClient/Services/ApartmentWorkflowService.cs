namespace Agency.ConsoleClient.Services
{
    using BLL.DTOs;

    public class ApartmentWorkflowService : IApartmentWorkflowService
    {
        private readonly IConsoleService _consoleService;

        private readonly IApartmentControllerService _apartmentControllerService;

        public ApartmentWorkflowService(IConsoleService consoleService, IApartmentControllerService apartmentControllerService)
        {
            _consoleService = consoleService;
            _apartmentControllerService = apartmentControllerService;
        }

        public void EditApartment()
        {
            ApartmentEditDto selectedApartment = _apartmentControllerService.PickApartmentForEdit();

            if (selectedApartment == null)
            {
                _consoleService.ReadKey();
                return;
            }

            ApartmentEditDto updatedApartment = _apartmentControllerService.UpdateApartment(selectedApartment);

            if (updatedApartment == null)
            {
                _consoleService.Print($"Failed to update apartment {selectedApartment.Id}");
                return;
            }

            _consoleService.ReadKey();
        }
    }
}
