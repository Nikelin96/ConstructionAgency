namespace Agency.DAL.EF
{
    using System.Data.Entity;
    using Agency.DAL.Model.Entities;

    public class DbInitializer : CreateDatabaseIfNotExists<ConstructionAgencyContext>
    {
        protected override void Seed(ConstructionAgencyContext db)
        {
            var apartment1 = new Apartment()
            {
                Name = "Flat1",
                RoomsCount = 1,
                State = ApartmentState.PartitionsDesigning
            };
            var apartment2 = new Apartment()
            {
                Name = "Flat2",
                RoomsCount = 2,
                State = ApartmentState.DrainageInstallation
            };
            var apartment3 = new Apartment()
            {
                Name = "Flat3",
                RoomsCount = 3,
                State = ApartmentState.FloorFitting
            };


            db.Apartments.AddRange(new[] { apartment1, apartment2, apartment3 });

            db.SaveChanges();
        }
    }
}