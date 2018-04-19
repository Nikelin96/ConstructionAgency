namespace Agency.BLL.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity.Core;
    using AutoMapper;
    using DAL.Interfaces;
    using DAL.Model.Entities;
    using DTOs;
    using Moq;
    using NUnit.Framework;
    using Services;

    [TestFixture]
    public class TestApartmentService
    {

        private IMapper Mapper { get; set; }


        public TestApartmentService()
        {
            Mapper = new Mapper(new MapperConfiguration
            (expression =>
            {
                expression.CreateMap<Apartment, ApartmentEditDto>();
                expression.CreateMap<ApartmentEditDto, Apartment>();
            }));
        }

        [Test]
        public void TestGetAll_Null_ReturnsAll()
        {
            //arrange
            List<Apartment> apartments = GetApartments(2);

            var mockRepository = new Mock<IRepository<Apartment>>();
            mockRepository.Setup(x => x.GetAll(null)).Returns(apartments);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(
                mapper => mapper.Map<IList<Apartment>, List<ApartmentEditDto>>(apartments)
                ).Returns(Mapper.Map<List<ApartmentEditDto>>(apartments));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Apartments).Returns(mockRepository.Object);

            IApartmentService apartmentService = new ApartmentService(mockUnitOfWork.Object, mockMapper.Object);

            //act
            IList<ApartmentEditDto> result = apartmentService.GetAll();

            //assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(apartments.First().Id, result.First().Id);
                Assert.AreEqual(apartments.Last().Id, result.Last().Id);
            });

            mockUnitOfWork.Verify(x => x.Apartments, Times.Once);
            mockRepository.Verify(x => x.GetAll(null), Times.Once);
            mockMapper.Verify(x => x.Map<IList<Apartment>, List<ApartmentEditDto>>(apartments), Times.Once);
        }

        [Test]
        public void TestUpdate_NotFound_ObjectNotFoundException()
        {
            //arrange
            ApartmentEditDto apartmentDto = Mapper.Map<ApartmentEditDto>(GetApartments(1).Single());

            var mockRepository = new Mock<IRepository<Apartment>>();
            mockRepository.Setup(x => x.Get(apartmentDto.Id)).Returns(() => null);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Apartments).Returns(mockRepository.Object);

            IApartmentService apartmentService = new ApartmentService(mockUnitOfWork.Object, null);

            //act
            //assert
            Assert.Throws<ObjectNotFoundException>(() =>
            {
                apartmentService.Update(apartmentDto);
            });

            mockUnitOfWork.Verify(x => x.Apartments, Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<Apartment>()), Times.Never);
        }

        [Test]
        public void TestUpdate_Null_NullReferenceException()
        {
            //arrange
            var mockRepository = new Mock<IRepository<Apartment>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Apartments).Returns(mockRepository.Object);

            IApartmentService apartmentService = new ApartmentService(mockUnitOfWork.Object, null);

            //act
            //assert
            Assert.Throws<NullReferenceException>(() =>
            {
                apartmentService.Update(null);
            });

            mockUnitOfWork.Verify(x => x.Apartments, Times.Once);
            mockRepository.Verify(x => x.Update(It.IsAny<Apartment>()), Times.Never);
        }


        [Test]
        public void TestUpdate_Valid_CallsRepository()
        {
            //arrange
            var apartmentDto = Mapper.Map<ApartmentEditDto>(GetApartments(1).Single());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map(It.IsAny<ApartmentEditDto>(), It.IsAny<Apartment>())).Returns(
                (ApartmentEditDto dto, Apartment apartment) => Mapper.Map(dto, apartment));

            var mockRepository = new Mock<IRepository<Apartment>>();
            mockRepository.Setup(x => x.Get(apartmentDto.Id)).Returns(Mapper.Map<Apartment>(apartmentDto));
            mockRepository.Setup(x => x.Update(It.IsAny<Apartment>()));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Apartments).Returns(mockRepository.Object);
            mockUnitOfWork.Setup(x => x.Commit());

            IApartmentService apartmentService = new ApartmentService(mockUnitOfWork.Object, mockMapper.Object);

            //act
            apartmentService.Update(apartmentDto);

            //assert
            mockUnitOfWork.Verify(x => x.Apartments, Times.Exactly(2));
            mockRepository.Verify(x => x.Update(It.IsAny<Apartment>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        private List<Apartment> GetApartments(int count = 0)
        {
            var appartments = new List<Apartment>
            {
                new Apartment {Id = 0},
                new Apartment {Id = 1},
                new Apartment {Id = 2},
                new Apartment {Id = 3},
                new Apartment {Id = 4},
                new Apartment {Id = 5},
            };

            return count > 0 ? appartments.GetRange(0, count) : appartments;
        }

    }
}
