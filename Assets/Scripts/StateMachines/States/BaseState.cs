using JG.FG.Interfaces;

namespace JG.FG.StateMachines
{
    public abstract class BaseState : IState<BaseState>
    {
        public bool stateIsFinished = false;
        public abstract BaseState NextState { get; }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public virtual BaseState GetNextState()
        {
            if (stateIsFinished is false)
                return this;

            return NextState;
        }
    }
}
