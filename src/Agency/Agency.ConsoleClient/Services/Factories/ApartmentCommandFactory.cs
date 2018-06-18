namespace Agency.ConsoleClient.Services.Factories
{
    using System;
    using BLL.DTOs;
    using BLL.Services;
    using Commands;
    using Ninject;
    using NLog;

    public class ApartmentCommandFactory : ICommandFactory<ApartmentEditDto>
    {
        private readonly IConsoleService _consoleService;

        private readonly IApartmentService _apartmentService;

        private readonly IApartmentStateService _apartmentStateService;

        private readonly Func<ILogger> _getLogger;

        public ApartmentCommandFactory(IConsoleService consoleService, IApartmentService apartmentService, IApartmentStateService apartmentStateService, Func<ILogger> getLogger)
        {
            _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
            _apartmentStateService = apartmentStateService ?? throw new ArgumentNullException(nameof(apartmentStateService));
            _getLogger = getLogger ?? throw new ArgumentNullException(nameof(getLogger));
        }

        private BaseCommand<ApartmentEditDto> CreateCommand(BaseCommand<ApartmentEditDto> currentCommand = null)
        {
            BaseCommand<ApartmentEditDto> resultCommand = null;

            if (currentCommand == null)
            {
                resultCommand = new CommandGetApartmentFromConsole(_consoleService, _apartmentService, _getLogger);
            }
            if (currentCommand is CommandGetApartmentFromConsole)
            {
                resultCommand = new CommandGetModifiedApartment(_consoleService, _apartmentStateService, currentCommand, _getLogger);
            }
            else if (currentCommand is CommandGetModifiedApartment)
            {
                resultCommand = new CommandUpdateAparment(_consoleService, _apartmentService, currentCommand, _getLogger);
            }

            return resultCommand;
        }

        public BaseCommand<ApartmentEditDto> ChainCommands()
        {
            // chain CommandGetApartmentFromConsole
            BaseCommand<ApartmentEditDto> command = CreateCommand();

            // chain CommandGetModifiedApartment
            command = CreateCommand(command);

            // chain CommandUpdateApartment
            command = CreateCommand(command);

            return command;
        }
    }
}
