namespace JG.FG.StateMachines
{
    public class IdleState : BaseState
    {
        private int counter = 1000;

        public override BaseState NextState => new MoveState();

        public override void Update()
        {
            counter--;

            if (counter <= 0)
                stateIsFinished = true;
        }
    }
}
