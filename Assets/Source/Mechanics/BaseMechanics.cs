using System.Collections;
using System.Collections.Generic;
using Source.Interfaces;
using Source.MovementStates;
using UnityEngine;

namespace Source.Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseMechanics : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected float _speed;
        [SerializeField] protected Transform _weaponPlace;
        [SerializeField] protected Weapon.Weapon _weaponClass;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected float _attackRange;
        protected IAttack _attack;
        private MovementStateMachine _movementStateMachine;
        private bool _pause = true;
        public MoveState MoveState { get; private set; }
        public IdleState IdleState { get; private set; }
        public BattleState BattleState { get; private set; }

        public bool CanAttack => CanAtkState();

        protected virtual void Update()
        {
            if (_pause) return;
            _movementStateMachine.GetCurrentMovementState.LogicUpdate();
            _movementStateMachine.GetCurrentMovementState.HandleInput();
        }

        protected virtual void FixedUpdate()
        {
            if (_pause) return;
            _movementStateMachine.GetCurrentMovementState.PhysicsUpdate();
        }

        public virtual void Initialize(List<Transform> enemies, float pauseTime, Weapon.Weapon weaponGameObject)
        {
            _weaponClass = Instantiate(weaponGameObject, _weaponPlace.position, _weaponPlace.rotation, _weaponPlace);
            _weaponClass.Setup(_attackSpeed);
            InitAttack(enemies, _weaponClass);
            InitMoveStateMachine();
            StartCoroutine(WaitForPause(pauseTime));
        }

        public virtual void Initialize(PlayerMechanics player, float pauseTime, Weapon.Weapon weaponGameObject)
        {
            _weaponClass = Instantiate(weaponGameObject, _weaponPlace.position, _weaponPlace.rotation, _weaponPlace);
            _weaponClass.Setup(_attackSpeed);
            InitAttack(player, _weaponClass);
            InitMoveStateMachine();
            StartCoroutine(WaitForPause(pauseTime));
        }

        protected abstract void InitAttack(List<Transform> enemies, Weapon.Weapon weapon);
        protected abstract void InitAttack(PlayerMechanics player, Weapon.Weapon weapon);

        private void InitMoveStateMachine()
        {
            _movementStateMachine = new MovementStateMachine();
            MoveState = new MoveState(this, _movementStateMachine);
            IdleState = new IdleState(this, _movementStateMachine, _attack);
            BattleState = new BattleState(this, _movementStateMachine, _attack);
            _movementStateMachine.Initialize(IdleState);
        }

        private IEnumerator WaitForPause(float time)
        {
            yield return new WaitForSeconds(time);
            _pause = false;
        }

        public virtual Vector3 GetMoveDirection()
        {
            return new Vector3();
        }

        public virtual bool CanAtkState()
        {
            return false;
        }

        public virtual void Move(Vector3 direction)
        {
            _rigidbody.velocity = new Vector3(GetMoveDirection().x * _speed, _rigidbody.velocity.y,
                GetMoveDirection().z * _speed);
            _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}