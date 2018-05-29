namespace Agency.ConsoleClient.Services
{
    using System;
    using BLL.DTOs;
    using Commands;
    using Factories;

    public class AgencyWorkflowService : IAgencyWorkflowService
    {
        private readonly IConsoleService _consoleService;
        private readonly ICommandFactory<ApartmentEditDto> _commandFactory;
        public AgencyWorkflowService(ICommandFactory<ApartmentEditDto> commandFactory, IConsoleService consoleService)
        {
            _commandFactory = commandFactory;
            _consoleService = consoleService;
        }

        public void StartEditLoop()
        {
            while (_consoleService.GetBool("Are you willing to proceed? (y/n)"))
            {
                _consoleService.Clear();

                try
                {
                    ICommand<ApartmentEditDto> apartmentCommand = GetCommand();

                    apartmentCommand.Execute();
                }
                catch (Exception e)
                {
                    _consoleService.Print(e);
                }
            }

            _consoleService.Print("Press any key to exit");
            _consoleService.ReadKey();
        }

        public ICommand<ApartmentEditDto> GetCommand()
        {
            // chain CommandGetApartmentFromConsole
            ICommand<ApartmentEditDto> command = _commandFactory.CreateCommand();

            // chain CommandGetModifiedApartment
            command = _commandFactory.CreateCommand(command);

            // chain CommandUpdateApartment
            command = _commandFactory.CreateCommand(command);

            return command;
        }
    }
}