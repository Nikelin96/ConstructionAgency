namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using BLL.DTOs;
    using BLL.Services;
    using DAL.Model.Entities;

    public class CommandGetModifiedApartment : ICommand<ApartmentEditDto>
    {
        private readonly IApartmentStateService _apartmentStateService;

        private readonly IConsoleService _consoleService;

        private readonly ICommand<ApartmentEditDto> _sourceCommand;

        public CommandGetModifiedApartment(IConsoleService consoleService, IApartmentStateService apartmentStateService, ICommand<ApartmentEditDto> sourceCommand)
        {
            _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentStateService = apartmentStateService ?? throw new ArgumentNullException(nameof(apartmentStateService));
            _sourceCommand = sourceCommand ?? throw new ArgumentNullException(nameof(sourceCommand));
        }

        public ApartmentEditDto Execute()
        {
            ApartmentEditDto apartmentEditDto = _sourceCommand.Execute();

            if (apartmentEditDto == null)
            {
                throw new ArgumentNullException(nameof(apartmentEditDto));
            }

            IEnumerable<ApartmentState> allowedStates = _apartmentStateService.GetAllowedApartmentStates(apartmentEditDto.State);

            if (!allowedStates.Any())
            {
                _consoleService.Print($"Apartment {apartmentEditDto.Id}, {apartmentEditDto.Name} is in final state {apartmentEditDto.State:G}");
                return apartmentEditDto;
            }

            _consoleService.Print("Set new Apartment Status:");

            _consoleService.Print(allowedStates);

            _consoleService.Print();

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            var newState = (ApartmentState)inputValue;

            (bool isValid, string message)
                validationResults = _apartmentStateService.Validate(apartmentEditDto, newState);

            if (!validationResults.isValid)
            {
                _consoleService.Print(validationResults.message);
                throw new ValidationException(validationResults.message);
            }

            apartmentEditDto.State = newState;

            return apartmentEditDto;
        }

    }
}
