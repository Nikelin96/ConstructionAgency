namespace Agency.ConsoleClient.Infrastructure
{
    using System;
    using AutoMapper;
    using BLL.DTOs;
    using BLL.Infrastructure;
    using BLL.Services;
    using DAL.EF;
    using DAL.Interfaces;
    using Ninject.Modules;
    using NLog;
    using Services;
    using Services.Factories;

    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(_connectionString);

            Bind<IMapper>().ToProvider<MapperProvider>().InSingletonScope();

            Bind<BLL.Services.IApartmentService>().To<BLL.Services.ApartmentService>();

            Bind<IApartmentStateService>().To<ApartmentStateService>();

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