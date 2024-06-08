namespace JG.FG.Interfaces
{
    public interface IState<T>
    {
        void Enter();
        void Update();
        void Exit();
        T GetNextState();
    }
}
