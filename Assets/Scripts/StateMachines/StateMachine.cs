namespace JG.FG.StateMachines
{
    public class StateMachine
    {
        private BaseState currentState;

        public StateMachine(BaseState initialState)
        {
            currentState = initialState;
            currentState.Enter();
        }

        public void ChangeState(BaseState newState)
        {
            if (currentState is not null)
                currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            if (currentState is null)
                return;

            currentState.Update();
            if (currentState.stateIsFinished is false)
                return;

            BaseState nextState = currentState.NextState;
            if (nextState != currentState)
                ChangeState(nextState);
        }
    }
}
