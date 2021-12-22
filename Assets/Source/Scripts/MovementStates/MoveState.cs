using Source.Scripts.Mechanics;

namespace Source.Scripts.MovementStates
{
    public class MoveState : State
    {
        public MoveState(BaseMechanics character, MovementStateMachine movementStateMachine) : base(character,
            movementStateMachine)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_character.GetMoveDirection().sqrMagnitude == 0f ||
                _character.CanAttack && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.IdleState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _character.Move(_character.GetMoveDirection());
        }
    }
}