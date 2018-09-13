namespace Agency.DAL.EF
{
    using System.Data.Entity;
    using Agency.DAL.Model.Entities;

    internal class DbInitializer : CreateDatabaseIfNotExists<ConstructionAgencyContext>
    {
        protected override void Seed(ConstructionAgencyContext db)
        {
//            var apartment1 = new Apartment()
//            {
//                Name = "Flat1",
//                RoomsCount = 1,
//                State = ApartmentState.PartitionsDesigning
//            };
//            var apartment2 = new Apartment()
//            {
//                Name = "Flat2",
//                RoomsCount = 2,
//                State = ApartmentState.DrainageInstallation
//            };
//            var apartment3 = new Apartment()
//            {
//                Name = "Flat3",
//                RoomsCount = 3,
//                State = ApartmentState.FloorFitting
//            };

//            db.Apartments.AddRange(new[] { apartment1, apartment2, apartment3 });
//
//            db.SaveChanges();

            var city = new City {Name = "Detroit"};

            db.Citites.Add(city);

            var district = new District {Name = "Brooklyn", City = city};

            db.Districts.Add(district);

            var street = new Street {Name = "Freedom", District = district};

            db.Streets.Add(street);

            var building = new Building {Street = street};

            db.Buildings.Add(building);

            var apartment = new Apartment
            {
                Name = "Luxury",
                RoomsCount = 3,
                State = ApartmentState.CeilingsDecoration,
                Building = building
            };

            db.Apartments.Add(apartment);

            var person = new Person
            {
                Name = "Tom",
                Age = 22,
                Apartment = apartment
            };

            db.Persons.Add(person);

            db.SaveChanges();
        }
    }
}