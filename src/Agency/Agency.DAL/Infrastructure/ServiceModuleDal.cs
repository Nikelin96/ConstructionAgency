namespace Agency.DAL.Infrastructure
{
    using EF;
    using Interfaces;
    using Ninject.Modules;

    public class ServiceModuleDal : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModuleDal(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(_connectionString);

        }
    }

}
