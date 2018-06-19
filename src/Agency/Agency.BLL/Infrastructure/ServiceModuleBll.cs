namespace Agency.BLL.Infrastructure
{
    using Agency.BLL.Services;
    using AutoMapper;
    using Ninject.Modules;

    public class ServiceModuleBll : NinjectModule
    {
        public ServiceModuleBll()
        { }

        public override void Load()
        {
            Bind<IMapper>().ToProvider<MapperProvider>().InSingletonScope();

            Bind<BLL.Services.IApartmentService>().To<BLL.Services.ApartmentService>();

            Bind<IApartmentStateService>().To<ApartmentStateService>();
        }
    }
}