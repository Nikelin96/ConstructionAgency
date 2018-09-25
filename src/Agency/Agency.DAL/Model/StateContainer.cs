namespace Agency.DAL.Model
{
    using System;
    using System.ComponentModel;

    public class StateContainer<T> : IStateContainer<T>
        where T : Enum
    {
        protected T _currentState;

        public virtual T State
        {
            get => _currentState;
            set
            {
                PrevState = _currentState;
                _currentState = value;
                OnStateChange?.Invoke(this);
            }
        }

        public T PrevState { get; private set; }

        public event Action<StateContainer<T>> OnStateChange;

        public StateContainer(T initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentNullException(nameof(initialState));
            }

            // validate initial State (here and in State setter)
            State = initialState;
            // everything is valid from now
            PrevState = State;
        }
    }
}