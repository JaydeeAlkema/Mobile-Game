namespace JG.FG.StateMachines
{
    public abstract class BaseState
    {
        public bool stateIsFinished = false;
        public virtual BaseState NextState { get; }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
