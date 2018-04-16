namespace Agency.DAL.Model
{
    using System;

    public interface IStateContainer
    {
        Enum State { get; set; }

        Enum PrevState { get; }

        event Action<StateContainer> OnStateChange;
    }
}