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


        public ApartmentControllerService(IConsoleService consoleService, IApartmentService apartmentService, IApartmentStateService apartmentStateService)
        {
            _consoleService = consoleService;
            _apartmentService = apartmentService;
            _apartmentStateService = apartmentStateService;
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

                return null;
            }

            if (!apartments.ContainsKey(inputValue))
            {
                _consoleService.Print("Element with such Index number does not exist");

                return null;
            }

            return apartments[inputValue];
        }


        public ApartmentEditDto UpdateApartment(ApartmentEditDto apartmentEditDto)
        {
            if (apartmentEditDto == null)
            {
                _consoleService.Print("Apartment is null");
                return null;
            }

            IEnumerable<ApartmentState> allowedStates = _apartmentStateService.GetAllowedApartmentStates(apartmentEditDto.State);

            if (!allowedStates.Any())
            {
                _consoleService.Print($"Apartment {apartmentEditDto.Id}, {apartmentEditDto.Name} is in final state {apartmentEditDto.State:G}");
                return null;
            }

            _consoleService.Print("Set new Apartment Status:");

            _consoleService.Print(allowedStates);

            _consoleService.Print();

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            if (inputValue == -1)
            {
                _consoleService.Print("Invalid number entered: only positive numbers and 0 are allowed");

                return null;
            }

            var newState = (ApartmentState)inputValue;

            (bool isValid, string message)
                validationResults = _apartmentStateService.Validate(apartmentEditDto, newState);

            if (!validationResults.isValid)
            {
                _consoleService.Print($"Cannot switch to the state: {newState}");
                _consoleService.Print(validationResults.message);
                return null;
            }

            apartmentEditDto.State = newState;
            _apartmentService.Update(apartmentEditDto);
            _consoleService.Print($"Apartment with id: {apartmentEditDto.Id} is successfully updated with status: {apartmentEditDto.State:G}");

            return apartmentEditDto;
        }
    }
}
