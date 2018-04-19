namespace Agency.ConsoleClient.Infrastructure
{
    using AutoMapper;
    using BLL.Infrastructure;
    using BLL.Services;
    using DAL.EF;
    using DAL.Interfaces;
    using Ninject.Modules;
    using Services;

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

            Bind<IApartmentService>()
                .To<ApartmentService>();

            Bind<IApartmentStateService>()
                .To<ApartmentStateService>();

            Bind<IConsoleService>()
                .To<ConsoleService>();
        }
    }
}