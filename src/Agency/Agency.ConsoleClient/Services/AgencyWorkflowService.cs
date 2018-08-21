namespace Agency.ConsoleClient.Services
{
    using System;
    using BLL.DTOs;
    using Commands;
    using Factories;
    using NLog;

    public class AgencyWorkflowService : IAgencyWorkflowService
    {
        private readonly IConsoleService _consoleService;
        private readonly ICommandFactory<ApartmentEditDto> _commandFactory;
        private readonly ILogger _logger;
        public AgencyWorkflowService(ICommandFactory<ApartmentEditDto> commandFactory, IConsoleService consoleService, ILogger logger)
        {
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start()
        {
            try
            {
                while (_consoleService.GetBool("Are you willing to proceed? (y/n)"))
                {

                    _logger.Info(Environment.NewLine);

                    _consoleService.Clear();

                    BaseCommand<ApartmentEditDto> apartmentCommand = _commandFactory.ChainCommands();

                    apartmentCommand.Execute();
                }
            }

            catch (Exception e)
            {
                _logger.Error(e);
                _consoleService.Print(e);
            }

            _consoleService.Print("Press any key to exit");
            _consoleService.ReadKey();
        }
    }
}