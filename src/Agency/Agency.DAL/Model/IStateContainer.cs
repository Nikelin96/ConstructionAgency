namespace Agency.DAL.Model
{
    using System;

    public interface IStateContainer<TState> where TState : Enum
    {
        TState State { get; set; }

        TState PrevState { get; }

        event Action<StateContainer<TState>> OnStateChange;
    }
}