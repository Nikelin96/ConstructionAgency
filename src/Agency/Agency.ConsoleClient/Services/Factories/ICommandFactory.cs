namespace Agency.ConsoleClient.Services.Factories
{
    using Agency.BLL.DTOs;
    using Commands;

    public interface ICommandFactory<T>
    {
        //ICommand<T> CreateCommand(ICommand<T> currentCommand = null);

        ICommand<ApartmentEditDto> ChainCommands();
    }
}
