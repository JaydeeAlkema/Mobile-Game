namespace JG.FG.StateMachines
{
    public class MoveState : BaseState
    {
        private int counter = 1000;

        public override BaseState NextState => new IdleState();

        public override void Enter()
        {
        }

        public override void Update()
        {
            counter--;

            if (counter <= 0)
                stateIsFinished = true;
        }

        public override void Exit()
        {
        }
    }
}
