namespace Agency.DAL.Model
{
    using System;
    using System.ComponentModel;

    public class StateContainer : IStateContainer
    {
        #region props

        // enum type for verification in runtime
        private readonly Type EnumType;

        private Enum _currentState;

        #region IStateContainer

        public Enum State
        {
            get => _currentState;
            set
            {
                Type newStateType = value.GetType();

                if (newStateType != EnumType)
                {
                    throw new InvalidEnumArgumentException($"{newStateType} does not match container type: {EnumType}");
                }

                PrevState = _currentState;
                _currentState = value;

                OnStateChange?.Invoke(this);
            }
        }

        public Enum PrevState { get; private set; }

        public event Action<StateContainer> OnStateChange;

        #endregion

        #endregion

        #region ctor

        public StateContainer(Enum initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentNullException(nameof(initialState));
            }

            // validate enumType
            EnumType = initialState.GetType();
            // validate initial State (here and in State setter)
            State = initialState;
            // everything is valid from now
            PrevState = State;
        }

        #endregion
    }
}