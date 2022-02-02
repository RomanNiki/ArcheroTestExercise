using Source.Mechanics;

namespace Source.MovementStates
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

            if (_character.Attack.HasEnemy()) _movementStateMachine.ChangeState(_character.BattleState);
        }
    }
}