namespace Agency.ConsoleClient.Services
{
    using System;
    using System.Collections.Generic;
    using BLL.DTOs;
    using DAL.Model.Entities;

    public interface IConsoleService
    {
        void Print(string text = null);
        void Print(Exception exception);
        void Print(IEnumerable<ApartmentState> states);
        void Print(IDictionary<int, ApartmentEditDto> apartments);
        void ReadKey();
        int GetInputAsNonNegativeNumber();
        bool GetBool();
        void Clear();
    }
}
