namespace Agency.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using Agency.DAL.Interfaces;

    public class EfGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        private DbContext _context { get; set; }
        private DbSet<TEntity> _dataSet { get; set; }

        public EfGenericRepository(DbContext context)
        {
            _context = context;
            _dataSet = _context.Set<TEntity>();
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> resultSet = _dataSet;

            if (filter != null)
            {
                resultSet = resultSet.Where(filter);
            }

            return resultSet.ToList();
        }

        public TEntity Get(int id)
        {
            TEntity entity = _dataSet.Find(id);

            return entity;
        }

        public void Add(TEntity item)
        {
            _dataSet.Add(item);
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            _dataSet.AddRange(items);
        }

        public void Update(TEntity item)
        {
//            DbEntityEntry<TEntity> entry = _context.Entry(item);
//            if (entry.State == EntityState.Detached)
//            {
//                _dataSet.Attach(item);
//            }
//
//            entry.State = EntityState.Modified;

//            DbEntityEntry<TEntity> entry = _context.Entry(item);

            var entityToUpdate = _dataSet.Find(item);

            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).State = EntityState.Modified;
                entityToUpdate = item;
            }
        }

        public bool Delete(int id)
        {
            TEntity entity = _dataSet.Find(id);
            if (entity == null) return false;

            _dataSet.Remove(entity);
            return true;
        }
    }
}