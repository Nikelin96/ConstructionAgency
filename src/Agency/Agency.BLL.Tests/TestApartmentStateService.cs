namespace Agency.BLL.Tests
{
    using Agency.BLL.DTOs;
    using Agency.BLL.Services;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class TestApartmentStateService
    {
        private IMapper Mapper { get; set; }

        public TestApartmentStateService()
        {
            Mapper = new Mapper(new MapperConfiguration
            (expression =>
            {
                expression.CreateMap<Apartment, ApartmentEditDto>();
                expression.CreateMap<ApartmentEditDto, Apartment>();
            }));
        }

        [Test]
        public void TestGetAllowedApartmentStates_NotFinalState_ListOfNextStates()
        {
            //arrange
            IApartmentStateService apartmentStateService = new ApartmentStateService();

            //act
            IEnumerable<ApartmentState> states = apartmentStateService.GetAllowedApartmentStates(ApartmentState.PartitionsDesigning);

            //assert
            Assert.Multiple(
                () =>
                {
                    Assert.IsFalse(states.Contains(ApartmentState.PartitionsDesigning));
                    Assert.Less(states.Count(), Enum.GetNames(typeof(ApartmentState)).Length);
                });
        }

        [Test]
        public void TestGetAllowedApartmentStates_FinalState_EmptyList()
        {
            //arrange
            IApartmentStateService apartmentStateService = new ApartmentStateService();

            //act
            IEnumerable<ApartmentState> states = apartmentStateService.GetAllowedApartmentStates(ApartmentState.Done);

            //assert
            Assert.IsEmpty(states);
        }

        [Test]
        public void TestValidate_NextState_True()
        {
            //arrange
            var apartmentDto = Mapper.Map<ApartmentEditDto>(new Apartment { Id = 0, State = ApartmentState.PartitionsDesigning });

            IApartmentStateService apartmentStateService = new ApartmentStateService();

            //act
            (bool isValid, string message) = apartmentStateService.Validate(apartmentDto, ApartmentState.DrainageInstallation);

            //assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(isValid);
                Assert.IsEmpty(message);
            });
        }

        [Test]
        public void TestValidate_PrevState_FalseWithNoAllowedStates()
        {
            //arrange
            var apartmentDto = Mapper.Map<ApartmentEditDto>(new Apartment { Id = 0, State = ApartmentState.Done });

            IApartmentStateService apartmentStateService = new ApartmentStateService();

            string expectedMessage =
                "Allowed States are:" +
                Environment.NewLine +
               string.Empty;

            //act
            (bool isValid, string message) = apartmentStateService.Validate(apartmentDto, ApartmentState.DrainageInstallation);

            //assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(isValid);
                Assert.AreEqual(message, expectedMessage);
            });
        }

        [Test]
        public void TestValidate_PrevState_False_ListOfAllowedStates()
        {
            //arrange
            const ApartmentState stateToVerify = ApartmentState.WallsDecoration;

            var apartmentDto = Mapper.Map<ApartmentEditDto>(new Apartment { Id = 0, State = stateToVerify });

            IEnumerable<ApartmentState> allowedStates = Enum.GetValues(typeof(ApartmentState)).OfType<ApartmentState>()
               .Where(state => (int)state > (int)stateToVerify);

            //act
            string expectedMessage =
                  "Allowed States are:" +
                  Environment.NewLine +
                  string.Join(
                      Environment.NewLine,
                      allowedStates.Select(state => $"{(int)state} {state:G}").ToArray()
                  );

            IApartmentStateService apartmentStateService = new ApartmentStateService();

            //act
            (bool isValid, string message) = apartmentStateService.Validate(apartmentDto, ApartmentState.SoundInsulation);

            //assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(isValid);
                Assert.AreEqual(message, expectedMessage);
            });
        }

    }
}
