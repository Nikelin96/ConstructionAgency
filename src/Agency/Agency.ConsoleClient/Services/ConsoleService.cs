﻿namespace Agency.ConsoleClient.Services
{
    using BLL.DTOs;
    using System;
    using System.Collections.Generic;
    using DAL.Model.Entities;

    public class ConsoleService : IConsoleService
    {
        #region IConsoleService
        public void Print(string text = null)
        {
            Console.WriteLine(text);
        }

        public void Print(Exception exception)
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine(exception);
        }

        public void Print(IEnumerable<ApartmentState> states)
        {
            foreach (ApartmentState state in states)
            {
                Print($"{(int)state}: {state:G}");
            }
        }

        public void Print(IEnumerable<ApartmentEditDto> apartments)
        {
            foreach (ApartmentEditDto record in apartments)
            {
                Print(record);
            }
        }

        public int GetInputAsNonNegativeNumber()
        {
            string input = Console.ReadLine()?.Trim();

            int value = int.TryParse(input, out int result) ? result : -1;

            if (value == -1)
            {
                Console.WriteLine("Invalid number entered: only positive numbers and 0 are allowed");

                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return value;
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }

        public bool GetBool(string message = "")
        {
            Console.WriteLine();
            Console.WriteLine(message);
            ConsoleKeyInfo input = Console.ReadKey();
            Console.WriteLine();

            char inputChar = char.ToLowerInvariant(input.KeyChar);

            if (inputChar != 'y' && inputChar != 'n')
            {
                throw new FormatException("Expected symbols are 'y' or 'n' ");
            }

            return inputChar == 'y';
        }
        public void Clear()
        {
            Console.Clear();
        }

        #endregion

        #region private
        private void Print(ApartmentEditDto apartment)
        {
            Print($"Id: {apartment.Id}");
            Print($"Name: {apartment.Name}");
            Print($"Rooms: {apartment.RoomsCount}");
            Print($"Repair State: {apartment.State:G}");
            Print();
        }

        #endregion
    }
}
