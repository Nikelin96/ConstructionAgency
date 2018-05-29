namespace Agency.ConsoleClient.Services.Commands
{
    public interface ICommand<out T>
    {
        T Execute();
    }
}
