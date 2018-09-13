namespace Agency.DAL.EF
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using Agency.DAL.Model.Entities;

    internal class ConstructionAgencyContext : DbContext
    {
        static ConstructionAgencyContext()
        {
            Database.SetInitializer<ConstructionAgencyContext>(new DbInitializer());
        }

        public ConstructionAgencyContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;

//            Database.SetInitializer(
//                new CreateDatabaseIfNotExists<ConstructionAgencyContext>());
        }

        public DbSet<City> Citites { get; set; }
        
        public DbSet<District> Districts { get; set; }
        
        public DbSet<Street> Streets { get; set; }
        
        public DbSet<Building> Buildings { get; set; }
        
        public DbSet<Apartment> Apartments { get; set; }
        
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Apartment> apartmentEntityConfig = modelBuilder.Entity<Apartment>();
        }
    }
}