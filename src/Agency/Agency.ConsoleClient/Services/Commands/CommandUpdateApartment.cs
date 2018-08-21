namespace Agency.ConsoleClient.Services.Commands
{
    using BLL.DTOs;
    using BLL.Services;
    using System;
    using NLog;

    public class CommandUpdateAparment : BaseCommand<ApartmentEditDto>
    {
        private readonly IApartmentService _apartmentService;

        private readonly IConsoleService _consoleService;

        private readonly BaseCommand<ApartmentEditDto> _sourceCommand;

        public CommandUpdateAparment(IConsoleService consoleService, IApartmentService apartmentService, BaseCommand<ApartmentEditDto> sourceCommand, Func<ILogger> getLogger)
            : base(getLogger())
        {
            _consoleService = consoleService;// ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentService = apartmentService;// ?? throw new ArgumentNullException(nameof(apartmentService));
            _sourceCommand = sourceCommand;// ?? throw new ArgumentNullException(nameof(sourceCommand));
        }

        public override ApartmentEditDto Execute()
        {
            _logger.Info("Begin Execution");

            _stopwatch.Start();

            _logger.Info("Calling Previous Command");

            ApartmentEditDto apartmentToUpdate = _sourceCommand.Execute();

            _logger.Info("Call Successful");

            if (apartmentToUpdate == null)
            {
                _logger.Warn("Previous Command returned null - nothing to update");
                _consoleService.Print($"Nothing to update");

                _stopwatch.Stop();

                _logger.Info($"End Execution in {_stopwatch.ElapsedMilliseconds}", apartmentToUpdate);

                return null;
            }

            _logger.Info("Updating apartment");

            _apartmentService.Update(apartmentToUpdate);

            _logger.Info($"Apartment with id: {apartmentToUpdate.Id} is successfully updated with status: {apartmentToUpdate.State:G}");

            _consoleService.Print($"Apartment with id: {apartmentToUpdate.Id} is successfully updated with status: {apartmentToUpdate.State:G}");

            _stopwatch.Stop();

            _logger.Info($"End Execution in {_stopwatch.ElapsedMilliseconds}", apartmentToUpdate);

            return apartmentToUpdate;
        }
    }
}
