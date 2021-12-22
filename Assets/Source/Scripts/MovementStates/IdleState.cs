using Source.Scripts.Mechanics;

namespace Source.Scripts.MovementStates
{
    public class IdleState : State
    {
        public IdleState(BaseMechanics character, MovementStateMachine movementStateMachine) : base(character,
            movementStateMachine)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_character.GetMoveDirection().sqrMagnitude > 0f ||
                _character.CanAttack == false && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.MoveState);

            if (_character.HaveEnemy()) _movementStateMachine.ChangeState(_character.BattleState);
        }
    }
}