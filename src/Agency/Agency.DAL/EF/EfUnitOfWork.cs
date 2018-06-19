namespace Agency.DAL.EF
{
    using System;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;

    internal class EfUnitOfWork : IUnitOfWork
    {
        #region private

        private readonly ConstructionAgencyContext _context;

        private IRepository<Apartment> _apartments { get; set; }

        #endregion

        #region constructor

        public EfUnitOfWork(string connectionString)
        {
            _context = new ConstructionAgencyContext(connectionString);
        }

        #endregion

        #region props

        public IRepository<Apartment> Apartments
        {
            get
            {
                if (_apartments != null) return _apartments;
                _apartments = new EfGenericRepository<Apartment>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _apartments;
            }
        }

        #endregion


        #region methods

        public void Commit()
        {
            _context.SaveChanges();
        }

        #endregion

        #region dispose

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}