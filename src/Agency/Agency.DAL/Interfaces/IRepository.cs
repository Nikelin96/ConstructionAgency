namespace Agency.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(int id);
        void Add(TEntity item);
        void AddRange(IEnumerable<TEntity> items);
        void Update(TEntity item);
        bool Delete(int id);
    }
}