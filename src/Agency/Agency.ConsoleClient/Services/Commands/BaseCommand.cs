namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using NLog;

    public abstract class BaseCommand<T>
    {
        protected readonly ILogger _logger;

        protected BaseCommand(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract T Execute();
    }
}
