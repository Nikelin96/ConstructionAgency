namespace Agency.DAL.Interfaces
{
    using System;
    using Agency.DAL.Model.Entities;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Apartment> Apartments { get; }
        void Commit();
    }
}