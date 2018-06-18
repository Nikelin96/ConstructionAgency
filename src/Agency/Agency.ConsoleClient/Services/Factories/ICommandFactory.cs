namespace Agency.ConsoleClient.Services.Factories
{
    using Agency.BLL.DTOs;
    using Commands;

    public interface ICommandFactory<T>
    {
        BaseCommand<ApartmentEditDto> ChainCommands();
    }
}
