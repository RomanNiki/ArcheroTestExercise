using Source.Mechanics;

namespace Source.MovementStates
{
    public class BattleState : State
    {
        public BattleState(BaseMechanics character, MovementStateMachine movementStateMachine)
            : base(character, movementStateMachine)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_character.Attack.HasEnemy() == false) _movementStateMachine.ChangeState(_character.IdleState);
            if (_character.GetMoveDirection().sqrMagnitude > 0f ||
                _character.CanAttack == false && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.MoveState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _character.Attack.TryOpenFire();
        }
    }
}