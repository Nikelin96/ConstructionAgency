//namespace Agency.ConsoleClient
//{
//    using System;
//    using System.Collections.Generic;
//    using Agency.BLL.DTOs;
//
//    public class OutputService
//    {
//        public void Print(IEnumerable<ApartmentEditDto> apartments)
//        {
//            foreach (ApartmentEditDto apartment in apartments)
//            {
//                Print(apartment);
//            }
//        }
//
//        private void Print(ApartmentEditDto apartment)
//        {
//            Console.WriteLine($"Id: {apartment.Id}");
//            Console.WriteLine($"Name: {apartment.Name}");
//            Console.WriteLine($"Rooms: {apartment.RoomsCount}");
//            Console.WriteLine($"Repair State: {apartment.State:G}");
//            Console.WriteLine();
//        }
//    }
//}