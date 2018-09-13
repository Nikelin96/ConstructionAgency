namespace Agency.DAL.EF
{
    using System;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;

    internal class EfUnitOfWork : IUnitOfWork
    {
        #region private

        private readonly ConstructionAgencyContext _context;

        private IRepository<City> _cities { get; set; }

        private IRepository<District> _districts { get; set; }

        private IRepository<Street> _streets { get; set; }

        private IRepository<Building> _buildings { get; set; }

        private IRepository<Apartment> _apartments { get; set; }

        private IRepository<Person> _persons { get; set; }

        #endregion

        #region constructor

        public EfUnitOfWork(string connectionString)
        {
            _context = new ConstructionAgencyContext(connectionString);
        }

        #endregion

        #region props

        public IRepository<City> Cities
        {
            get
            {
                if (_cities != null) return _cities;
                _cities = new EfGenericRepository<City>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _cities;
            }
        }

        public IRepository<District> Districts
        {
            get
            {
                if (_districts != null) return _districts;
                _districts = new EfGenericRepository<District>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _districts;
            }
        }

        public IRepository<Street> Streets
        {
            get
            {
                if (_streets != null) return _streets;
                _streets = new EfGenericRepository<Street>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _streets;
            }
        }

        public IRepository<Building> Buildings
        {
            get
            {
                if (_buildings != null) return _buildings;
                _buildings = new EfGenericRepository<Building>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _buildings;
            }
        }

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

        public IRepository<Person> Persons
        {
            get
            {
                if (_persons != null) return _persons;
                _persons = new EfGenericRepository<Person>(_context);

                //_comments.RetrieveHandler += UpdateViews;
                return _persons;
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