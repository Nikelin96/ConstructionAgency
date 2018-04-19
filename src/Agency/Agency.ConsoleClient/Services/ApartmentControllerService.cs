namespace Agency.ConsoleClient.Services
{
    using BLL.DTOs;
    using BLL.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Model.Entities;

    public class ApartmentControllerService : IApartmentControllerService
    {
        private readonly IApartmentService _apartmentService;

        private readonly IApartmentStateService _apartmentStateService;

        private readonly IConsoleService _consoleService;


        public ApartmentControllerService(IApartmentService apartmentService, IApartmentStateService apartmentStateService, IConsoleService consoleService)
        {
            _apartmentService = apartmentService;
            _apartmentStateService = apartmentStateService;
            _consoleService = consoleService;
        }

        public ApartmentEditDto PickApartmentForEdit()
        {
            _consoleService.Print("List of appartments: ");

            var index = 0;
            Dictionary<int, ApartmentEditDto> apartments = _apartmentService.GetAll().ToDictionary(key => ++index, value => value);
            _consoleService.Print(apartments);

            _consoleService.Print("Select Apartment: ");

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();
            if (inputValue == -1)
            {
                _consoleService.Print("Invalid number entered: only positive numbers and 0 are allowed");

                // todo replace this with prompt to continue or not
                _consoleService.ReadKey();
                return null;
            }

            if (!apartments.ContainsKey(inputValue))
            {
                _consoleService.Print("Element with such Index number does not exist");

                // todo replace this with prompt to continue or not
                _consoleService.ReadKey();
                return null;
            }

            return apartments[inputValue];
        }


        public ApartmentEditDto UpdateApartment(ApartmentEditDto selectedApartment)
        {
            IEnumerable<ApartmentState> allowedStates = _apartmentStateService.GetAllowedApartmentStates(selectedApartment.State);

            if (!allowedStates.Any())
            {
                _consoleService.Print($"Apartment {selectedApartment.Id}, {selectedApartment.Name} is in it's final state {selectedApartment.State:G}");
                return null;
            }

            _consoleService.Print("Set new Apartment Status:");

            _consoleService.Print(allowedStates);

            _consoleService.Print();

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            if (inputValue == -1)
            {
                _consoleService.Print("Invalid number entered: only positive numbers and 0 are allowed");

                // todo replace this with prompt to continue or not
                _consoleService.ReadKey();
                return null;
            }

            var newState = (ApartmentState)inputValue;

            (bool isValid, string message)
                validationResults = _apartmentStateService.Validate(selectedApartment, newState);

            if (!validationResults.isValid)
            {
                _consoleService.Print($"Cannot switch to the state: {newState}");
                _consoleService.Print(validationResults.message);
            }
            else
            {
                selectedApartment.State = newState;
                _apartmentService.Update(selectedApartment);
                _consoleService.Print($"Apartment with id: {selectedApartment.Id} is successfully updated with status: {selectedApartment.State:G}");
            }

            return selectedApartment;
        }
    }
}
