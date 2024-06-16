namespace JG.FG.StateMachines
{
    public class MoveState : BaseState
    {
        private int counter = 1000;

        public override BaseState NextState => new IdleState();

        public override void Update()
        {
            counter--;

            if (counter <= 0)
                stateIsFinished = true;
        }
    }
}
