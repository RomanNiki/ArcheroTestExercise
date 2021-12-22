using Source.Scripts.Mechanics;

namespace Source.Scripts.MovementStates
{
    public class State
    {
        protected readonly BaseMechanics _character;
        protected readonly MovementStateMachine _movementStateMachine;

        public State(BaseMechanics character, MovementStateMachine movementStateMachine)
        {
            _character = character;
            _movementStateMachine = movementStateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}