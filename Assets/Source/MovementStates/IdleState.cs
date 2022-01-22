using Source.Interfaces;
using Source.Mechanics;

namespace Source.MovementStates
{
    public class IdleState : State
    {
        private IAttack _attack;
        public IdleState(BaseMechanics character, MovementStateMachine movementStateMachine, IAttack attack) : base(character,
            movementStateMachine)
        {
            _attack = attack;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_character.GetMoveDirection().sqrMagnitude > 0f ||
                _character.CanAttack == false && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.MoveState);

            if (_attack.HasEnemy()) _movementStateMachine.ChangeState(_character.BattleState);
        }
    }
}