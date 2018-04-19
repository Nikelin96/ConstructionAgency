namespace Agency.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Agency.BLL.DTOs;
    using Agency.BLL.Services;
    using Agency.ConsoleClient.Services;
    using ApartmentState = DAL.Model.Entities.ApartmentState;

    public class AgencyTerminal
    {
        private readonly IApartmentService _apartmentService;

        private readonly IApartmentStateService _apartmentStateService;

        private readonly IConsoleService _consoleService;

        public AgencyTerminal(IApartmentService apartmentService, IApartmentStateService apartmentStateService, IConsoleService consoleService)
        {
            _apartmentService = apartmentService;
            _apartmentStateService = apartmentStateService;
            _consoleService = consoleService;
        }

        public void Start()
        {
            while (GetPermissionToProceed())
            {
                ApartmentEditDto selectedApartment = PickApartmentForEdit();

                if (selectedApartment == null)
                {
                    continue;
                }

                // todo replace with navigation in the application
                _consoleService.Print("Press any key to continue");
                _consoleService.ReadKey();
                _consoleService.Print();

                ApartmentEditDto updatedApartment = UpdateApartment(selectedApartment);

                if (updatedApartment == null)
                {
                    _consoleService.Print($"Failed to update apartment {selectedApartment.Id}");
                    continue;
                }

                _consoleService.ReadKey();
            }

            _consoleService.Print("Press any key to exit");
            _consoleService.ReadKey();
        }

        public bool GetPermissionToProceed()
        {
            var isAllowed = false;

            _consoleService.Clear();
            _consoleService.Print($"Are you willing to proceed? (y/n)");

            try
            {
                isAllowed = _consoleService.GetBool();
            }
            catch (FormatException e)
            {
                _consoleService.Print(e);
                _consoleService.ReadKey();
                return isAllowed; // error is thrown so it's still false
            }

            return isAllowed;
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
                _consoleService.Print($"Apartment id {selectedApartment.Id} is successfully updated with status: {selectedApartment.State:G}");
            }

            return selectedApartment;
        }

    }
}