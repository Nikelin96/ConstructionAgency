namespace Agency.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Agency.BLL.DTOs;
    using Agency.BLL.Services;
    using Agency.DAL.EF;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;
    using DotNetCraft.Common.DataAccessLayer.UnitOfWorks.SimpleUnitOfWorks;
    using Ninject;

    public class AgencyTerminal
    {
        private readonly IApartmentService _apartmentService;

//        private readonly OutputService _outputService;

        public AgencyTerminal(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        public void Start()
        {
            Console.WriteLine("List of appartments:");

            IList<ApartmentEditDto> apartments = _apartmentService.GetAll();

            Print(apartments);

            Console.Write($"Select Apartment: ");

            ApartmentEditDto selectedApartment = null;
            int inputId = GetInputAsDigit();

            if (inputId > -1)
            {
                selectedApartment = apartments.FirstOrDefault(x => x.Id == inputId);
            }

            if (selectedApartment != null)
            {
                Console.WriteLine($"Selected apartment with Id :{selectedApartment.Id}");
                Console.WriteLine($"Apartment Status: {selectedApartment.State:G}");
            }
            else
            {
                return;
            }

            Console.WriteLine("Press any key to continue");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine($"Set new Apartment Status:");

            foreach (ApartmentState state in _apartmentService.GetAllowedApartmentStates(selectedApartment.State))
            {
                Console.WriteLine($"{(int) state}: {state:G}");
            }

            var inputState = (ApartmentState) GetInputAsDigit();

            (bool isValid, string message)
                validationResults = _apartmentService.Validate(selectedApartment, inputState);

            if (validationResults.isValid)
            {
                selectedApartment.State = inputState;
                _apartmentService.Update(selectedApartment);
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine($"Cannot switch to the state: {inputState}");
                Console.WriteLine(validationResults.message);
            }

            Console.ReadKey();
        }

        private int GetInputAsDigit()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();
            return char.IsDigit(input.KeyChar) ? Int32.Parse(input.KeyChar.ToString()) : -1;
        }

        private void Print(IEnumerable<ApartmentEditDto> apartments)
        {
            foreach (ApartmentEditDto apartment in apartments)
            {
                Print(apartment);
            }
        }

        private void Print(ApartmentEditDto apartment)
        {
            Console.WriteLine($"Id: {apartment.Id}");
            Console.WriteLine($"Name: {apartment.Name}");
            Console.WriteLine($"Rooms: {apartment.RoomsCount}");
            Console.WriteLine($"Repair State: {apartment.State:G}");
            Console.WriteLine();
        }
    }
}