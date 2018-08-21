namespace Agency.ConsoleClient.Services.Commands
{
    using System;
    using System.Diagnostics;
    using NLog;

    public abstract class BaseCommand<T>
    {
        protected readonly ILogger _logger;

        protected readonly Stopwatch _stopwatch;

        protected BaseCommand(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _stopwatch = new Stopwatch();
        }

        public abstract T Execute();
    }
}
