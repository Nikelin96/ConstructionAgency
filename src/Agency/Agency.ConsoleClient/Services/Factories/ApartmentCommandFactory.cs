﻿namespace Agency.ConsoleClient.Services.Factories
{
    using System;
    using BLL.DTOs;
    using BLL.Services;
    using Commands;
    using Ninject;

    public class ApartmentCommandFactory : ICommandFactory<ApartmentEditDto>
    {
        private readonly IConsoleService _consoleService;

        private readonly IApartmentService _apartmentService;

        private readonly IApartmentStateService _apartmentStateService;

        public ApartmentCommandFactory(IConsoleService consoleService, IApartmentService apartmentService, IApartmentStateService apartmentStateService)
        {
            _consoleService = consoleService;
            _apartmentService = apartmentService;
            _apartmentStateService = apartmentStateService;
        }

        //public ICommand<ApartmentEditDto> CreateCommand()
        //{
        //    ICommand<ApartmentEditDto> resultCommand = CreateCommand(null);
        //    resultCommand = CreateCommand(resultCommand);

        //    return resultCommand;
        //}


        public ICommand<ApartmentEditDto> CreateCommand(ICommand<ApartmentEditDto> currentCommand = null)
        {
            ICommand<ApartmentEditDto> resultCommand = null;

            if (currentCommand is CommandGetApartmentFromConsole)
            {
                resultCommand = new CommandGetModifiedApartment(_consoleService, _apartmentStateService, currentCommand);
            }
            else if (currentCommand is CommandGetModifiedApartment)
            {
                resultCommand = new CommandUpdateAparment(_consoleService, _apartmentService, currentCommand);
            }
            else if (currentCommand == null)
            {
                resultCommand = new CommandGetApartmentFromConsole(_consoleService, _apartmentService);
            }

            return resultCommand;
        }
    }
}
