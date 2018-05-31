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

        public void Start()
        {
            while (_consoleService.GetBool("Are you willing to proceed? (y/n)"))
            {
                _consoleService.Clear();

                try
                {
                    ICommand<ApartmentEditDto> apartmentCommand = _commandFactory.ChainCommands();

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
    }
}