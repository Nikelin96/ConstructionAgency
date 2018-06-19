namespace Agency.ConsoleClient.Infrastructure
{
    using System;
    using BLL.DTOs;
    using Ninject.Modules;
    using NLog;
    using Services;
    using Services.Factories;

    public class ServiceModuleConsoleClient : NinjectModule
    {
        public ServiceModuleConsoleClient()
        { }

        public override void Load()
        {
            Bind<IConsoleService>().To<ConsoleService>();

            Bind<ICommandFactory<ApartmentEditDto>>().To<ApartmentCommandFactory>();

            Bind<IAgencyWorkflowService>().To<AgencyWorkflowService>();

            SetupLogger();

            Bind<ILogger>().ToMethod(context => NLog.LogManager.GetCurrentClassLogger());

            Bind<Func<ILogger>>().ToMethod(context => NLog.LogManager.GetCurrentClassLogger);
        }

        public void SetupLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logs.txt" };
            //var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            NLog.LogManager.Configuration = config;
        }
    }
}