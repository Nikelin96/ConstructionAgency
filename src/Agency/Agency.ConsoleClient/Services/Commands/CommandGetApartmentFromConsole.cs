namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using BLL.Services;
    using System.Collections.Generic;
    using System.Linq;
    using BLL.DTOs;

    public class CommandGetApartmentFromConsole : ICommand<ApartmentEditDto>
    {
        private readonly IApartmentService _apartmentService;

        private readonly IConsoleService _consoleService;

        public CommandGetApartmentFromConsole(IConsoleService consoleService, IApartmentService apartmentService)
        {
            _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
        }

        public ApartmentEditDto Execute()
        {
            _consoleService.Print("List of appartments: ");

            var index = 0;
            Dictionary<int, ApartmentEditDto> apartments = _apartmentService.GetAll().ToDictionary(key => ++index, value => value);
            _consoleService.Print(apartments);

            _consoleService.Print("Select Apartment: ");

            int inputValue = _consoleService.GetInputAsNonNegativeNumber();

            if (!apartments.ContainsKey(inputValue))
            {
                _consoleService.Print("Element with such Index number does not exist");

                return null;
            }

            return apartments[inputValue];
        }
    }
}
