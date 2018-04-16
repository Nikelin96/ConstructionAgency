namespace Agency.BLL.Infrastructure
{
    using System;
    using Agency.BLL.DTOs;
    using Agency.DAL.Model.Entities;
    using AutoMapper;
    using Ninject.Activation;

    public class MapperProvider : IProvider<IMapper>
    {
        public Type Type => typeof(IMapper);

        public object Create(IContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                // comments
                cfg.CreateMap<Apartment, ApartmentEditDto>();
                cfg.CreateMap<ApartmentEditDto, Apartment>();
            });

            return config.CreateMapper();
        }
    }
}