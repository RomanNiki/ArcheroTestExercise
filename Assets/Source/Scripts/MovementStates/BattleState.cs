using System.Collections.Generic;
using Source.Scripts.Mechanics;
using UnityEngine;

namespace Source.Scripts.MovementStates
{
    public class BattleState : State
    {
        protected List<GameObject> _enemies;

        public BattleState(BaseMechanics character, MovementStateMachine movementStateMachine, List<GameObject> enemies)
            : base(character, movementStateMachine)
        {
            _enemies = enemies;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_enemies.Count == 0) _movementStateMachine.ChangeState(_character.IdleState);
            if (_character.GetMoveDirection().sqrMagnitude > 0f ||
                _character.CanAttack == false && _character is EnemyBaseMechanics)
                _movementStateMachine.ChangeState(_character.MoveState);
        }

        public override void Enter()
        {
            base.Enter();
            _character.RotateToEnemy();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _character.TryOpenFire();
        }
    }
}