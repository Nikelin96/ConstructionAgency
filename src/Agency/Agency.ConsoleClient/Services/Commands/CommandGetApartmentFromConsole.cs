namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using BLL.Services;
    using System.Collections.Generic;
    using System.Linq;
    using BLL.DTOs;
    using NLog;

    public class CommandGetApartmentFromConsole : BaseCommand<ApartmentEditDto>
    {
        private readonly IApartmentService _apartmentService;

        private readonly IConsoleService _consoleService;

        public CommandGetApartmentFromConsole(IConsoleService consoleService, IApartmentService apartmentService, Func<ILogger> getLogger)
            : base(getLogger())
        {
            _consoleService = consoleService;// ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentService = apartmentService;// ?? throw new ArgumentNullException(nameof(apartmentService));
        }

        public override ApartmentEditDto Execute()
        {
            _logger.Info("Begin Execution");
            _consoleService.Print("List of appartments: ");

            var index = 0;
            Dictionary<int, ApartmentEditDto> apartments = _apartmentService.GetAll().ToDictionary(key => ++index, value => value);
            _consoleService.Print(apartments);

            _consoleService.Print("Select Apartment: ");

            _logger.Info("Retrieving number from console");

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            _logger.Info("Retrieve Successful");

            if (!apartments.ContainsKey(inputValue))
            {
                _logger.Warn($"Element by Serial Number: {inputValue} does not exist");

                _consoleService.Print($"Element by Serial Number: {inputValue} does not exist");

                return null;
            }

            _logger.Info("End Execution", apartments[inputValue]);
            return apartments[inputValue];
        }
    }
}
