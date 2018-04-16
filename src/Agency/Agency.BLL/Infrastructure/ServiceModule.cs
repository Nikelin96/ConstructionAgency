namespace Agency.BLL.Infrastructure
{
    using Agency.BLL.Services;
    using Agency.DAL.EF;
    using Agency.DAL.Interfaces;
    using AutoMapper;
    using Ninject.Modules;

    public class ServiceModule : NinjectModule
    {
        private string _connectionString;

        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(_connectionString);

            Bind<IMapper>().ToProvider<MapperProvider>().InSingletonScope();

            Bind<IApartmentService>()
                .To<ApartmentService>(); //.WithConstructorArgument(typeof(IUnitOfWork), typeof(IMapper));

//            Bind<IApartmentService>().To<IApartmentService>();
        }
    }
}