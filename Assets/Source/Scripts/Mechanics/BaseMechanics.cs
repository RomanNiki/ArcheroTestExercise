using System.Collections;
using System.Collections.Generic;
using Source.Scripts.MovementStates;
using UnityEngine;

namespace Source.Scripts.Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseMechanics : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected float _speed;
        [SerializeField] protected Transform _weaponPlace;
        [SerializeField] protected Scripts.Weapon.Weapon _weaponClass;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected float _attackRange;
        protected List<GameObject> _enemies;
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

        public virtual void Initialize(List<GameObject> enemies, float pauseTime, GameObject weaponGameObject)
        {
            if (enemies != null) _enemies = enemies;
            _weaponClass = Instantiate(weaponGameObject, _weaponPlace.position, _weaponPlace.rotation, _weaponPlace)
                .GetComponent<Scripts.Weapon.Weapon>();
            _weaponClass.Setup(_attackSpeed);
            _movementStateMachine = new MovementStateMachine();
            MoveState = new MoveState(this, _movementStateMachine);
            IdleState = new IdleState(this, _movementStateMachine);
            BattleState = new BattleState(this, _movementStateMachine, _enemies);
            _movementStateMachine.Initialize(IdleState);
            StartCoroutine(WaitForPause(pauseTime));
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

        public abstract void TryOpenFire();

        public virtual bool HaveEnemy()
        {
            return _enemies.Count > 0;
        }

        public virtual void RotateToEnemy()
        {
        }

        protected virtual bool CanAtkState()
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