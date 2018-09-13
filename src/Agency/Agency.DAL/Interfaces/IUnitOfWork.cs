namespace Agency.DAL.Interfaces
{
    using System;
    using Agency.DAL.Model.Entities;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<City> Cities { get; }

        IRepository<District> Districts { get; }

        IRepository<Street> Streets { get; }

        IRepository<Building> Buildings { get; }

        IRepository<Apartment> Apartments { get; }

        IRepository<Person> Persons { get; }

        void Commit();
    }
}