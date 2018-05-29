namespace Agency.ConsoleClient.Services.Factories
{
    using Commands;

    public interface ICommandFactory<T>
    {
        ICommand<T> CreateCommand(ICommand<T> currentCommand = null);
    }
}
