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

            IList<ApartmentEditDto> apartments = _apartmentService.GetAll();
            _consoleService.Print(apartments);

            _consoleService.Print("Select Apartment: ");

            _logger.Info("Retrieving number from console");

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            _logger.Info("Retrieve Successful");

            ApartmentEditDto apartment = apartments.SingleOrDefault(x => x.Id == inputValue);

            if (apartment == null)
            {
                _logger.Warn($"Element by Id: {inputValue} does not exist");

                _consoleService.Print($"Element by Id: {inputValue} does not exist");
            }

            _logger.Info("End Execution", apartment);

            return apartment;
        }
    }
}
