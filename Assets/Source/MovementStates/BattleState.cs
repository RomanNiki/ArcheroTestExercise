using Source.Interfaces;
using Source.Mechanics;

namespace Source.MovementStates
{
    public class BattleState : State
    {
        protected IAttack _attack;

        public BattleState(BaseMechanics character, MovementStateMachine movementStateMachine, IAttack attack)
            : base(character, movementStateMachine)
        {
            _attack = attack;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_attack.HasEnemy() == false) _movementStateMachine.ChangeState(_character.IdleState);
            if (_character.GetMoveDirection().sqrMagnitude > 0f ||
                _character.CanAttack == false && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.MoveState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _attack.TryOpenFire();
        }
    }
}