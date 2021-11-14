namespace Source.MovementStates
{
    public class MovementStateMachine
    {
        public State GetCurrentMovementState { get; private set; }

        public void Initialize(State startingMovementState)
        {
            GetCurrentMovementState = startingMovementState;
            startingMovementState.Enter();
        }

        public void ChangeState(State newMovementState)
        {
            GetCurrentMovementState.Exit();

            GetCurrentMovementState = newMovementState;
            newMovementState.Enter();
        }
    }
}