namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using BLL.DTOs;
    using BLL.Services;
    using DAL.Model.Entities;
    using NLog;

    public class CommandGetModifiedApartment : BaseCommand<ApartmentEditDto>
    {
        private readonly IApartmentStateService _apartmentStateService;

        private readonly IConsoleService _consoleService;

        private readonly BaseCommand<ApartmentEditDto> _sourceCommand;

        public CommandGetModifiedApartment(IConsoleService consoleService, IApartmentStateService apartmentStateService, BaseCommand<ApartmentEditDto> sourceCommand, Func<ILogger> getLogger)
            : base(getLogger())
        {
            _consoleService = consoleService;// ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentStateService = apartmentStateService;// ?? throw new ArgumentNullException(nameof(apartmentStateService));
            _sourceCommand = sourceCommand;// ?? throw new ArgumentNullException(nameof(sourceCommand));
        }

        public override ApartmentEditDto Execute()
        {
            _logger.Info("Begin Execution");
            _logger.Info("Calling Previous Command");

            ApartmentEditDto apartmentEditDto = _sourceCommand.Execute();

            _logger.Info("Call Successful");

            if (apartmentEditDto == null)
            {
                throw new ArgumentNullException(nameof(apartmentEditDto));
            }

            _logger.Info("Retrieving Allowed States");

            IEnumerable<ApartmentState> allowedStates = _apartmentStateService.GetAllowedApartmentStates(apartmentEditDto.State);

            _logger.Info("Retrieve Successful");

            if (!allowedStates.Any())
            {
                _logger.Warn($"Apartment {apartmentEditDto.Id}, {apartmentEditDto.Name} is in final state {apartmentEditDto.State:G}");
                _consoleService.Print($"Apartment {apartmentEditDto.Id}, {apartmentEditDto.Name} is in final state {apartmentEditDto.State:G}");
                //return apartmentEditDto;
                return null;
            }

            _consoleService.Print("Set new Apartment Status:");

            _consoleService.Print(allowedStates);

            _consoleService.Print();

            _logger.Info("Retrieving number from console");

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            _logger.Info("Retrieve Successful");

            var newState = (ApartmentState)inputValue;

            (bool isValid, string message)
                validationResults = _apartmentStateService.Validate(apartmentEditDto, newState);

            if (!validationResults.isValid)
            {
                _consoleService.Print(validationResults.message);
                throw new ValidationException(validationResults.message);
            }

            apartmentEditDto.State = newState;

            _logger.Info("End Execution", apartmentEditDto);
            return apartmentEditDto;
        }

    }
}
