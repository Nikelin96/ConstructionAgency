namespace Agency.ConsoleClient.Services.Commands
{
    using BLL.DTOs;
    using BLL.Services;
    using System;

    public class CommandUpdateAparment : ICommand<ApartmentEditDto>
    {
        private readonly IApartmentService _apartmentService;

        private readonly IConsoleService _consoleService;

        private readonly ICommand<ApartmentEditDto> _sourceCommand;

        public CommandUpdateAparment(IConsoleService consoleService, IApartmentService apartmentService, ICommand<ApartmentEditDto> sourceCommand)
        {
            _consoleService = consoleService ?? throw new ArgumentNullException(nameof(consoleService));
            _apartmentService = apartmentService ?? throw new ArgumentNullException(nameof(apartmentService));
            _sourceCommand = sourceCommand ?? throw new ArgumentNullException(nameof(sourceCommand));
        }

        public ApartmentEditDto Execute()
        {
            ApartmentEditDto apartmentToUpdate = _sourceCommand.Execute();
            if (apartmentToUpdate == null)
            {
                throw new ArgumentNullException(nameof(apartmentToUpdate));
            }

            _apartmentService.Update(apartmentToUpdate);
            _consoleService.Print($"Apartment with id: {apartmentToUpdate.Id} is successfully updated with status: {apartmentToUpdate.State:G}");

            return apartmentToUpdate;
        }
    }
}
