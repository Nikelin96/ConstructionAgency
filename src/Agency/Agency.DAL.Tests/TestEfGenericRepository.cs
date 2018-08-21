namespace Agency.DAL.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class TestEfGenericRepository
    {
        //public TestEfGenericRepository()
        //{

        //}

        //[Test]
        //public void TestGetAll()
        //{
        //    // arrange
        //    IQueryable<TestEntity> testData = new List<TestEntity>() { new TestEntity(), new TestEntity() }.AsQueryable();

        //    var mockSet = new Mock<DbSet<TestEntity>>();
        //    mockSet.Setup(x => x.AsNoTracking().AsQueryable()).Returns(testData);

        //    var mockContext = new Mock<DbContext>();
        //    mockContext.Setup(x => x.Set<TestEntity>()).Returns(mockSet.Object);

        //    var genericRepository = new EfGenericRepository<TestEntity>(mockContext.Object);

        //    // act
        //    IList<TestEntity> result = genericRepository.GetAll();

        //    // assert
        //    CollectionAssert.AreEqual(testData, result);
        //}

        //[Test]
        //public void TestGet()
        //{
        //    // arrange
        //    IQueryable<TestEntity> testData = new List<TestEntity>() { new TestEntity(), new TestEntity() }.AsQueryable();

        //    var mockSet = new Mock<DbSet<TestEntity>>();
        //    mockSet.Setup(x => x.AsNoTracking().AsQueryable()).Returns(testData);

        //    var mockContext = new Mock<DbContext>();
        //    mockContext.Setup(x => x.Set<TestEntity>()).Returns(mockSet.Object);

        //    // act
        //    var genericRepository = new EfGenericRepository<TestEntity>(mockContext.Object);

        //    // assert
        //    IList<TestEntity> result = genericRepository.Get(0);
        //}


    }
}
