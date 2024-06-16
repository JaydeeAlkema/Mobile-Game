namespace JG.FG.StateMachines
{
    public class StateMachine
    {
        public event System.Action<BaseState> OnStateStart;
        public event System.Action<BaseState, BaseState> OnStateChange;

        private BaseState previousState;
        private BaseState currentState;

        public BaseState CurrentState => currentState;

        public StateMachine(BaseState initialState)
        {
            currentState = initialState;
        }

        public void Initialize()
        {
            currentState.Enter();
            OnStateStart?.Invoke(currentState);
        }

        public void ChangeState(BaseState newState)
        {
            if (currentState is not null)
            {
                currentState.Exit();
                previousState = currentState;
            }

            currentState = newState;
            currentState.Enter();

            OnStateChange?.Invoke(previousState, currentState);
        }

        public void Update()
        {
            if (currentState is null)
                return;

            currentState.Update();
            if (currentState.stateIsFinished is false)
                return;

            BaseState nextState = currentState.NextState;
            if (nextState == currentState)
                return;

            ChangeState(nextState);
        }
    }
}
